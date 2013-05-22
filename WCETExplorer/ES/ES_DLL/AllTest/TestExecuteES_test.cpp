#include <CppUTest\TestHarness.h>
#include <executeES.h>
#include <functionConstraints.h>

TEST_GROUP(testES0)
{
	float analog[10];
	bool digital[40];
	enum SIGNALTYP typ[10];

	void setup()
	{
		analog[0] = 1.1f;
		analog[1] = 2.2f;
	};

	void teardown()
	{
	};
};

TEST(testES0,NOT_NORMED_OVER)
{
	double actual = testES0(1, analog, 0, 0, 0, 0);
	DOUBLES_EQUAL(TEST_ES_0_MAX_ANALOG_0, actual, 0.01);
};

TEST(testES0,NOT_NORMED_UNDER)
{
	analog[0]=-0.1f;
	double actual = testES0(1, analog, 0, 0, 0, 0);
	DOUBLES_EQUAL(TEST_ES_0_MIN_ANALOG_0,actual, 0.01);
};

TEST(testES0,NORMED_VALUE_RECALCULATE)
{
	analog[0]=0.1f;
	double actual = testES0(1, analog, 0, 0, 0, 0);
	DOUBLES_EQUAL(5,actual, 0.01);
};

TEST(testES0,WRONG_USING)
{
	double actual = testES0(0, analog, 0, 0, 0, 0);
	DOUBLES_EQUAL(ERROR_CODE_USING,actual, 0.01);
};

//-------------------------------------------------------------

TEST_GROUP(testES1){};

TEST(testES1,ALLWAYS_ZERO)
{
	double actual = testES1(0, 0, 0, 0, 0, 0);
	DOUBLES_EQUAL(0, actual, 0.0f);
};

//-------------------------------------------------------------

TEST_GROUP(testES2){};

TEST(testES2,ALLWAYS_ONE)
{
	double actual = testES2(0, 0, 0, 0, 0, 0);
	DOUBLES_EQUAL(1, actual, 0.0f);
};

//-------------------------------------------------------------

TEST_GROUP(testES3)
{
	float analog[10];
	bool digital[40];
	enum SIGNALTYP typ[10];

	void setup()
	{
		analog[0] = 1.1f;
		digital[0] = true;
		typ[0] = SIGNALTYP::SINUS;
	};

	void teardown()
	{
	};
};

TEST(testES3, SUM_ANALOG_DIGITAL_SIGNAL)
{
	double actual = testES3(1, analog, 1, digital, 1, typ);
	DOUBLES_EQUAL(7.1f, actual, 0.0001f);
};

TEST(testES3, WRONG_USING)
{
	double actual = testES3(0, analog, 1, digital, 1, typ);
	DOUBLES_EQUAL(ERROR_CODE_USING, actual, 0.0001f);
};
