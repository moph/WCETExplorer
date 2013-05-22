#include <CppUTest\TestHarness.h>
#include <executeES.h>
#include <functionConstraints.h>

TEST_GROUP(executeES0)
{
	float analog[10];
	bool digital[40];
	enum SIGNALTYP typ[10];

	void setup()
	{
		analog[0] = 0.6f;
		analog[1] = 2.2f;

		digital[0] = true;

		typ[0] = SIGNALTYP::RECHTECK;
	};

	void teardown()
	{
	};
};

TEST(executeES0,NOT_NORMED_CALL)
{
	double actual = executeES0(2,analog, 1, digital, 1, typ);
	DOUBLES_EQUAL(-3.0 , actual, 0.01f);
}

TEST(executeES0,NORMAL_CALL)
{
	analog[1] = 0.8f;
	double actual = executeES0(2,analog, 1, digital, 1, typ);
	DOUBLES_EQUAL(14460.0f , actual, 0.01f);
}

TEST(executeES0,WRONG_USING_ANALOG)
{
	double actual = executeES0(1,analog, 1, digital, 1, typ);
	DOUBLES_EQUAL(ERROR_CODE_USING , actual, 0.01f);
}

TEST(executeES0,WRONG_USING_DIGITAL)
{
	double actual = executeES0(1,analog, 0, digital, 1, typ);
	DOUBLES_EQUAL(ERROR_CODE_USING , actual, 0.01f);
}

TEST(executeES0,WRONG_USING_SIGNAL)
{
	double actual = executeES0(1,analog, 1, digital, 100, typ);
	DOUBLES_EQUAL(ERROR_CODE_USING , actual, 0.01f);
}

TEST(executeES0,MAX_VALUE)
{
	analog[0] = 1;
	analog[1] = 1;
	typ[0] = SIGNALTYP::PWM;
	double actual = executeES0(2,analog, 1, digital, 1, typ);
	DOUBLES_EQUAL(200109.0f , actual, 0.01f);
}

TEST(executeES0,MIN_VALUE)
{
	analog[1] = 0.5f;			//Area from -100 100 so middle value 0.5 is 0 in the area
	typ[0] = SIGNALTYP::RECHTECK;
	double actual = executeES0(2,analog, 1, digital, 1, typ);
	DOUBLES_EQUAL(0.0f , actual, 0.01f);
}