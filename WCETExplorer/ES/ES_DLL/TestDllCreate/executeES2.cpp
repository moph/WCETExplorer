#include "executeES.h"
#include <math.h>
#include "functionConstraints.h"
#include <limits>



//********************************************
//Definition of executeES2
//
//	/*Dämpfung z = abs(x*sin(y)*p/d)
//********************************************
double executeES2(int sizeAnalogXML, 
				 float analog[],
				 int sizeDigitalXML,
				 bool digital[],
				 int sizeSignalXML, 
				 enum SIGNALTYP typ[])
{
	double ret = 0.0;
	double x = 0;
	double y = 0;
	double p = 0;
	double d = 0;
	
	ret = checkSizeOfParameter(sizeAnalogXML, sizeDigitalXML, sizeSignalXML, 3, 0, 1);
	if(ret != 0){
		return ret;
	}


	if (!isEnumInRange(typ[0], 0, ES2_MAX_P - ES2_MIN_P))
	{
		return ERROR_CODE_USING;
	}
	p = (double) (typ[0] + ES2_MIN_P );

	ret = setAnalogValue(analog[0], ES2_MIN_X, ES2_MAX_X);
	if(ret == 0)
	{
		x = analog[0];
	}
	
	ret = setAnalogValue(analog[1], ES2_MIN_Y, ES2_MAX_Y);
	if(ret == 0)
	{
		y = analog[1];
	}

	ret = setAnalogValue(analog[2], ES2_MIN_D, ES2_MAX_D);
	if(ret == 0)
	{
		d = analog[2];
	}

	if (ret == 0)
	{
		ret = abs(x*sin(y)*p/d);
	}

	return ret;
}