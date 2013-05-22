#include "executeES.h"
#include "functionConstraints.h"

//********************************************
//Definition of test functions
//
//********************************************
double testES0(int sizeAnalogXML, 
				 float analog[],
				 int sizeDigitalXML,
				 bool digital[],
				 int sizeSignalXML, 
				 enum SIGNALTYP typ[])
{
	double ret = 0.0;

	ret = checkSizeOfParameter(sizeAnalogXML,sizeDigitalXML,sizeSignalXML, 1, 0, 0);
	if(ret != 0){
		return ret;
	}

	if ( isValueBetween_0_1(analog[0])){
		ret = calculateFunctionValue(analog[0],TEST_ES_0_MIN_ANALOG_0, TEST_ES_0_MAX_ANALOG_0);
	}
	else if(analog[0] > 1){
		ret = TEST_ES_0_MAX_ANALOG_0;
	}
	else{
		ret = TEST_ES_0_MIN_ANALOG_0;
	}

	return ret;
}

double testES1(int sizeAnalogXML, 
				 float analog[],
				 int sizeDigitalXML,
				 bool digital[],
				 int sizeSignalXML, 
				 enum SIGNALTYP typ[])
{
	double ret = 0.0;
	return ret;
}

double testES2(int sizeAnalogXML, 
				 float analog[],
				 int sizeDigitalXML,
				 bool digital[],
				 int sizeSignalXML, 
				 enum SIGNALTYP typ[])
{
	double ret = 1.0;
	return ret;
}

double testES3(int sizeAnalogXML, 
				 float analog[],
				 int sizeDigitalXML,
				 bool digital[],
				 int sizeSignalXML, 
				 enum SIGNALTYP typ[])
{
	double ret = 0.0;

	ret = checkSizeOfParameter(sizeAnalogXML, sizeDigitalXML, sizeSignalXML, 1, 1, 1);
	if(ret != 0){
		return ret;
	}
	
	if (!isEnumInRange(typ[0], TEST_ES3_ENUM_MIN, TEST_ES3_ENUM_MAX)){
		return ERROR_CODE_USING;
	}
	
	ret = analog[0] + digital[0] + typ[0];

	return ret;
}