#include <CppUTest\TestHarness.h>
#include <executeES.h>
#include <functionConstraints.h>

TEST_GROUP(executeES2)
{
	float analog[10];
	bool digital[40];
	enum SIGNALTYP typ[10];

	void setup()
	{
		analog[0] = 1.0;			//x
		analog[1] = 36.2787f/38.0;	//y
		analog[2] = 0.0f;			//d

		typ[0] = SIGNALTYP::EMP;	//p
	}

};

TEST(executeES2, WRONG_USING)
{
	double actual = executeES2(2, analog, 0, digital, 2, typ);

	DOUBLES_EQUAL(ERROR_CODE_USING, actual, 0.01f);
}

TEST(executeES2, MAX_VALUE)
{
	double actual = executeES2(3, analog, 0, digital, 1, typ);

	DOUBLES_EQUAL(2000.0f, actual, 0.01f);
}

TEST(executeES2, MAX_VALUE_WITH_ABS)
{
	analog[0] = 0;
	analog[1] = (19-17.2787)/38;
	analog[2] = 0;

	double actual = executeES2(3, analog, 0, digital, 1, typ);

	DOUBLES_EQUAL(2000.0f, actual, 0.01f);
}

TEST(executeES2, MIN_VALUE)
{
	analog[0] = 0.5;
	analog[1] = 0.5;
	analog[2] = 1;

	typ[0] = SIGNALTYP::DREIECK;

	double actual = executeES2(3, analog, 0, digital, 1, typ);

	DOUBLES_EQUAL(0.0f, actual, 0.01f);
}


TEST(executeES2, ENUM_BEGIN_AT_ZERO)
{
	analog[0] = 0.0;
	analog[1] = 0.0;
	analog[2] = 0.0;

	typ[0] = SIGNALTYP::RECHTECK;

	double actual = executeES2(3, analog, 0, digital, 1, typ);

	DOUBLES_EQUAL(149.88f, actual, 0.01f);

}