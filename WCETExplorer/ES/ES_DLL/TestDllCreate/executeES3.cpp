#include "executeES.h"
#include <math.h>
#include "functionConstraints.h"
#include <limits>
void calcPolyroots(bool switches[], int start, int* pPolyroots);

double executeES3(int sizeAnalogXML, 
				 float analog[],
				 int sizeDigitalXML,
				 bool digital[],
				 int sizeSignalXML, 
				 enum SIGNALTYP typ[])
{

 
	 double ret = 0;
	 double doubles[10];
	 double tmp;
	 int enums[10];
	 int polyroots[8];
	 int i,j;
    
   // check Parameter size 
   ret = checkSizeOfParameter(sizeAnalogXML, sizeDigitalXML, sizeSignalXML, 10, 40, 10);
	if(ret != 0){
		return ret;
	}
   
   // check parameter range and calculate analog values
   for (i = 0; i < 10; i++){
   
      if(!isEnumInRange(typ[i], ES3_MIN_SIGNAL ,ES3_MAX_SIGNAL)){
      
		return ERROR_CODE_USING;
      
      }
      enums[i] = (int)typ[i];
      
      ret = setAnalogValue(analog[i], ES3_MIN_ANALOG, ES3_MAX_ANALOG, doubles[i]);
      if(ret != 0){
         return ret;
      }
   }
	 
	 for (i = 0; i < 10; i++){
      //changed switches to digital
		calcPolyroots(digital, i, polyroots);
		tmp = enums[i]-4;
		for (j=0; j<8; j++){
			tmp *= doubles[i]-polyroots[j];
		}
		ret += tmp;
	 } 
	 
	 return ret;
	 }


void calcPolyroots(bool switches[40], int start, int* pPolyroots){
	int i,j;
	int result = 0;
	for(i=0; i < 8; i++){
		for(j=0; j < 5; j++){
			result += switches[(start+i+5-j)%40]*pow(2.0, j);
		}
		if (switches[start+i] == 1)
      {
         pPolyroots[i] = -1 * result;
      } else {
         pPolyroots[i] = result;
      }
	}
}
