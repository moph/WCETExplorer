#include "executeES.h"
#include <math.h>
#include "functionConstraints.h"
#include <limits>

double executeES3(int sizeAnalogXML, 
				 float analog[],
				 int sizeDigitalXML,
				 bool digital[],
				 int sizeSignalXML, 
				 enum SIGNALTYP typ[])
{

/* NOT FINISHED
	 double ret = 0;
	 bool switches[40];
	 double doubles[10];
	 double tmp;
	 int enums[10];
	 int polyroots[8];
	 int i,j;
	 
	 for (i = 0; i < 10; i++){
		calcPolyroots(switches, i, &polyroots);
		tmp = enums[i]-4;
		for (j=0; j<8; j++){
			tmp *= doubles[i]-polyroots[j];
		}
		ret += tmp;
	 } */
	 double ret = 0;
	 return ret;
	 }
/*
void calcPolyroots(bool switches[40], int start, int* pPolyroots){
	int i,j;
	int result = 0;
	for(i=0; i <=8; i++){
		for(j=0; j <5, j++){
			result += switches[(start+i+5-j)%40]*pow(2, j);
		}
		
		pPolyroots[i] = (-switches[start+i])*result;
	}
}	*/ 
