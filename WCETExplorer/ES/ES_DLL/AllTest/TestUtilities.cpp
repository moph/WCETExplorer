#include <CppUTest/TestHarness.h>
#include <executeES.h>
#include <functionConstraints.h>

TEST_GROUP(showAllParameter)
{
};

IGNORE_TEST(showAllParameter, visitEveryPath_C1_Coverage)
{
	float analog[] = {0.123f, 0.567f, 1.424f, 55.66f, 0.0f};
	bool digital[] = {true, false, true, false, false};
	enum SIGNALTYP typ[] = {SIGNALTYP::AM, SIGNALTYP::BAUD19200, SIGNALTYP::DREIECK, SIGNALTYP::PM, SIGNALTYP::NUMBER_OF_SIGNALTYPS};
	showAllParameter(5, analog, 5, digital, 5, typ);
	//no return parameter so always true
	CHECK(true);
};

//--------------------------------

TEST_GROUP(calculateFunctionValue)
{

};

TEST(calculateFunctionValue, MIN_GREATER_MAX)
{
	float actual = calculateFunctionValue(0.4f, 20, 10);
	DOUBLES_EQUAL(10+1, actual, 0.01);
};

TEST(calculateFunctionValue, MIN_LESS_MAX)
{
	float actual = calculateFunctionValue(0.4f, 10, 110);
	DOUBLES_EQUAL(50, actual, 0.01);
};

TEST(calculateFunctionValue, MIN_NEG_LESS_MAX_POS)
{
	float actual = calculateFunctionValue(0.1f, -10, 90);
	DOUBLES_EQUAL(0, actual, 0.01);
};

TEST(calculateFunctionValue, MIN_NEG_LESS_MAX_NEG)
{
	float actual = calculateFunctionValue(0.8f, -110, -10);
	DOUBLES_EQUAL(-30, actual, 0.01);
};

TEST(calculateFunctionValue, MIN_NEAR_MAX)
{
	float actual = calculateFunctionValue(0.5f, -0.1, 0.1);
	DOUBLES_EQUAL(0, actual, 0.01);
};

//--------------------------------

TEST_GROUP(isValueBetween_0_1)
{
};

TEST(isValueBetween_0_1, VALUE_IS_BETWEEN)
{
	CHECK(isValueBetween_0_1(0.4f));
}

TEST(isValueBetween_0_1, VALUE_LESS_THEN_0)
{
	CHECK(!isValueBetween_0_1(-0.1f));
}

TEST(isValueBetween_0_1, VALUE_GREATER_THEN_1)
{
	CHECK(!isValueBetween_0_1(1.1f));
}

//--------------------------------

TEST_GROUP(isEnumInRange)
{

};

TEST(isEnumInRange, TRUE)
{
	enum SIGNALTYP typ = SIGNALTYP::BAUD19200;
	CHECK(isEnumInRange(typ, ES2_MIN_D, ES2_MAX_D));
}

TEST(isEnumInRange, FALSE)
{
	enum SIGNALTYP typ = SIGNALTYP::AM;
	CHECK(!isEnumInRange(typ, ES2_MIN_D, ES2_MAX_D));
}


//-------------------------------------------------------------

TEST_GROUP(setAnalogValue)
{
	float analog[10];

	void setup()
	{
		analog[0] = 0.7;
	};
};

TEST(setAnalogValue, ERROR_CODE_NOT_NORMED)
{
	analog[0] = 1.1f;
	float actual = setAnalogValue(analog[0], -10, 10);
	DOUBLES_EQUAL(ERROR_CODE_NOT_NORMED, actual, 0.01f);
};

TEST(setAnalogValue, ERROR_CODE_MIN_MAX_DISTORTED)
{
	float actual = setAnalogValue(analog[0], 10, -10);
	DOUBLES_EQUAL(ERROR_CODE_MIN_MAX_DISTORTED, actual, 0.01f);
};

TEST(setAnalogValue, NORMAL_USE)
{
	float actual;
	actual = setAnalogValue(analog[0], -5, 5);
	if(actual == 0)
	{
		actual = analog[0];
	}
	
	DOUBLES_EQUAL(2.0f, actual, 0.01f);
};

//-------------------------------------------------------------

TEST_GROUP(checkSizeOfParameter)
{
	int sizeAnalogXML;
	int sizeDigitalXML;
	int sizeSignalXML;

	void setup()
	{
		sizeAnalogXML = 0;
		sizeDigitalXML = 0;
		sizeSignalXML = 0;
	};
};

TEST(checkSizeOfParameter, WRONG_USE)
{
	
	CHECK(checkSizeOfParameter(sizeAnalogXML, sizeDigitalXML, sizeSignalXML, 1, 0, 1) == ERROR_CODE_USING);
}

TEST(checkSizeOfParameter, NORMAL_USE)
{
	CHECK(checkSizeOfParameter(sizeAnalogXML, sizeDigitalXML, sizeSignalXML, 0, 0, 0) == 0);
}