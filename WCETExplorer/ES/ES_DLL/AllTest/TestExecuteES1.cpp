#include <CppUTest\TestHarness.h>
#include <executeES.h>
#include <functionConstraints.h>

TEST_GROUP(executeES1)
{
	float analog[10];
	bool digital[40];
	enum SIGNALTYP typ[10];

	void setup()
	{
		digital[0] = true;
		digital[1] = true;
		digital[2] = true;
		digital[3] = true;
		digital[4] = true;
		digital[5] = true;
		digital[6] = true;
		digital[7] = true;
		digital[8] = true;
		digital[9] = true;
	};

	void teardown()
	{
	};
};

TEST(executeES1,MAX_VALUE)
{
	double actual = executeES1(0,analog, 10, digital, 0, typ);
	DOUBLES_EQUAL(DBL_MAX_10 , actual, 0.01f);
}

TEST(executeES1,WRONG_USING)
{
	double actual = executeES1(0,analog, 9, digital, 0, typ);
	DOUBLES_EQUAL(ERROR_CODE_USING , actual, 0.01f);
}

TEST(executeES1,WRONG_USING_MAX_PARA)
{
	digital[10] = true;
	double actual = executeES1(0,analog, 33, digital, 0, typ);
	DOUBLES_EQUAL(ERROR_CODE_USING , actual, 0.01f);
}

TEST(executeES1,INFINIT_VALUE)
{
	digital[10] = true;
	double actual = executeES1(0,analog, 11, digital, 0, typ);
	DOUBLES_EQUAL(DBL_MAX_10 , actual, 0.01f);
}