#include "executeES.h"
#include <stdio.h>

void showAllParameter(const int sizeAnalogXML, 
						 const float analog[],
						 const int sizeDigitalXML,
						 const bool digital[],
						 const int sizeSignalXML, 
						 const enum SIGNALTYP typ[])
{
	printf("\n\n ------ sizeAnalogXML %d ------ ",sizeAnalogXML);
	for(int i = 0; i < sizeAnalogXML; i++)
	{
		printf("\n %f",analog[i]);
	}

	printf("\n\n ------sizeDigitalXML %d ------ ",sizeDigitalXML);
	for(int i = 0; i < sizeDigitalXML; i++)
	{
		printf("\n %s", digital[i] ? "True":"False");
	}

	printf("\n\n ------sizeSignalXML %d ------ ",sizeSignalXML);
	for(int i = 0; i < sizeSignalXML; i++)
	{
		printf("\n %d",typ[i]);
	}

	return;
}

bool isValueBetween_0_1(const float value)
{
	if(value >= 0 && value <= 1){
		return true;
	}
	return false;
}

float calculateFunctionValue(const float value,const float Min, const float Max)
{
	float res;
	float diff = Max-Min;

	if(Min >= Max){
		return Max+1;
	}

	res = Min+value*diff;

	return res;
}

float setAnalogValue(float analog, const float Min, const float Max, double &returnValue)
{
	float ret = 0.0;
	if(isValueBetween_0_1(analog)){
			returnValue = calculateFunctionValue(analog, Min, Max);
			if(ret > Max)
			{ //error 
				ret = ERROR_CODE_MIN_MAX_DISTORTED;
			}
	}
	else{
		ret = ERROR_CODE_NOT_NORMED;
	}
	return ret;
};

int checkSizeOfParameter(const int IsSizeAnalogXML,const int IsSizeDigitalXML, const  int IsSizeSignalXML,
						 const int ExpectedSizeAnalogXML, const  int ExpectedSizeDigitalXML, const  int ExpectedSizeSignalXML
	)
{
	if(!(IsSizeAnalogXML >= ExpectedSizeAnalogXML 
		&& IsSizeDigitalXML >= ExpectedSizeDigitalXML 
		&& IsSizeSignalXML >= ExpectedSizeSignalXML))
	{
		return ERROR_CODE_USING;
	}
	else
	{
		return 0;
	}
}

bool isEnumInRange(const enum SIGNALTYP typ, const int MIN, const int MAX)
{
	if(MIN <= typ && typ <= MAX)
	{
		return true;
	}
	return false;
};