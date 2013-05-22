#include "executeES.h"
#include <math.h>
#include "functionConstraints.h"
#include <limits>



//********************************************
//Definition of executeES1
//	10 <= Digitalvalues
//
//	/*Exponential z = 2^x*/
//********************************************
double executeES1(int sizeAnalogXML, 
				 float analog[],
				 int sizeDigitalXML,
				 bool digital[],
				 int sizeSignalXML, 
				 enum SIGNALTYP typ[])
{
	double ret = 0.0;
	const double basis = 2.0;
	unsigned int t = 0;
	
	ret = checkSizeOfParameter(sizeAnalogXML, sizeDigitalXML, sizeSignalXML, 0, 10, 0);
	if(ret != 0){
		return ret;
	}

	if(sizeDigitalXML > 32)
		return ERROR_CODE_USING;

	for(int i = 0; i < sizeDigitalXML ; i++)
	{
		if(digital[i] == true)
		{
			t |= 1 << i;
		}
	}

	ret = pow(basis,(double)t);
	if(numeric_limits<double>::infinity() == ret)
	{
		ret = DBL_MAX_10;
	}

	return ret;
}