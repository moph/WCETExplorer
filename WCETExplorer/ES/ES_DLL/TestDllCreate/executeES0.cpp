#include "executeES.h"
#include <math.h>
#include "functionConstraints.h"

//********************************************
//Definition of executeES0
//
//	z  = abs(a*x^2+b*x+c)
//********************************************
double executeES0(int sizeAnalogXML, 
				 float analog[],
				 int sizeDigitalXML,
				 bool digital[],
				 int sizeSignalXML, 
				 enum SIGNALTYP typ[])
{
	double ret = 0.0;
	float a = 0;
	float b = 0;
	float c = 0;
	float x = 0;

	//showAllParameter(sizeAnalogXML, analog, sizeDigitalXML, digital, sizeSignalXML, typ);
	
	ret = checkSizeOfParameter(sizeAnalogXML, sizeDigitalXML, sizeSignalXML, 2, 1, 1);
	if(ret != 0){
		return ret;
	}

	if(!isEnumInRange(typ[0], ES0_MIN_C ,ES0_MAX_C)){
		return ERROR_CODE_USING;
	}

	c = (float)typ[0];

	b = (float)digital[0];

	ret = setAnalogValue(analog[0], ES0_MIN_A, ES0_MAX_A);
	if(ret == 0){
		a = analog[0];
	}
		
	ret = setAnalogValue(analog[1], ES0_MIN_X, ES0_MAX_X);
	if(ret == 0){ //Error in setAnalogValue
		x = analog[1];
	}


	if(!(ret < 0))
	{
		ret = abs(a* pow(x,2)) + abs(b*x) + abs(c);
	}

	return ret;
}




