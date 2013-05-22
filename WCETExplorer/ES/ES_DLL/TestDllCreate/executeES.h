#include <iostream>
#include <windows.h>

using namespace std;

#define ERROR_CODE_USING -1
#define ERROR_CODE_INDEX_OUT_OF_BOUND -2
#define ERROR_CODE_NOT_NORMED -3
#define ERROR_CODE_MIN_MAX_DISTORTED -4

//Attention: NUMBER_OF_SIGNALTYPS must be at minimum 2 
enum SIGNALTYP{
	RECHTECK,
	DREIECK,
	SAEGEZAHN,
	BAUD19200,
	BAUD9600,
	SINUS,
	AM,
	FM,
	PM,
	PWM,
	EMP,
	NUMBER_OF_SIGNALTYPS
};


/// <summary>
/// Determines whether enum is in range.
/// </summary>
/// <param name="typ">The Signaltyp.</param>
/// <param name="Min">The MIN.</param>
/// <param name="MAX">The MAX.</param>
/// <returns>true/false</returns>
extern "C" __declspec(dllexport) bool __cdecl isEnumInRange(const enum SIGNALTYP typ, const int MIN, const int MAX);

/// <summary>
/// Checks the size of parameter.
/// </summary>
/// <param name="IsSizeAnalogXML">The is size analog XML.</param>
/// <param name="IsSizeDigitalXML">The is size digital XML.</param>
/// <param name="IsSizeSignalXML">The is size signal XML.</param>
/// <param name="ExpectedSizeAnalogXML">The expected size analog XML.</param>
/// <param name="ExpectedSizeDigitalXML">The expected size digital XML.</param>
/// <param name="ExpectedSizeSignalXML">The expected size signal XML.</param>
/// <param name="">The .</param>
/// <returns>0 or ERROER_CODE_USING</returns>
extern "C" __declspec(dllexport) int __cdecl checkSizeOfParameter(const int IsSizeAnalogXML, const int IsSizeDigitalXML, const int IsSizeSignalXML,
						const int ExpectedSizeAnalogXML, const int ExpectedSizeDigitalXML, const int ExpectedSizeSignalXML
	);

/// <summary>
/// Sets the analog value.
/// </summary>
/// <param name="analog">The analog.</param>
/// <param name="Min">The min.</param>
/// <param name="Max">The max.</param>
/// <returns>RealValue or errorcode</returns>
extern "C" __declspec(dllexport) float __cdecl setAnalogValue(float &analog, const float Min, const float Max);

/// <summary>
/// calculate the normed value into die area of MAX MIN
/// </summary>
/// <param name="value">normed value</param>
/// <param name="Min">Min Value</param>
/// <param name="Max">Max Value</param>
/// <returns>number between Min and Max</returns>
extern "C" __declspec(dllexport) float __cdecl calculateFunctionValue(const float value, const  float Min, const float Max);

/// <summary>
/// is value normed
/// </summary>
/// <param name="checkValue">check if the value is normed</param>
/// <returns>true/false</returns>
extern "C" __declspec(dllexport) bool __cdecl isValueBetween_0_1(const float value);

/// <summary>
/// print all parameters into stream 'STDIN' with printf
/// </summary>
/// <param name="sizeAnalogXML">The size analog XML.</param>
/// <param name="analog">The analog array.</param>
/// <param name="sizeDigitalXML">The size digital XML.</param>
/// <param name="digital">The digital array.</param>
/// <param name="sizeSignalXML">The size signal XML.</param>
/// <param name="typ">The enum-typ array.</param>
/// <returns>void</returns>
extern "C" __declspec(dllexport) void __cdecl showAllParameter(const int sizeAnalogXML, 
						 const float analog[],
						 const int sizeDigitalXML,
						 const bool digital[],
						 const int sizeSignalXML, 
						 const enum SIGNALTYP typ[]);

/// <summary>
/// calculate a parabel
/// </summary>
/// <param name="sizeAnalogXML">The size analog XML.</param>
/// <param name="analog">The analog array.</param>
/// <param name="sizeDigitalXML">The size digital XML.</param>
/// <param name="digital">The digital array.</param>
/// <param name="sizeSignalXML">The size signal XML.</param>
/// <param name="typ">The enum-typ array.</param>
/// <returns>double as fitness</returns>
extern "C" __declspec(dllexport) double __cdecl  executeES0(int sizeAnalogXML, 
									 float analog[],
									 int sizeDigitalXML,
									 bool digital[],
									 int sizeSignalXML, 
									 enum SIGNALTYP typ[]);

/// <summary>
/// ????????????
/// </summary>
/// <param name="sizeAnalogXML">The size analog XML.</param>
/// <param name="analog">The analog array.</param>
/// <param name="sizeDigitalXML">The size digital XML.</param>
/// <param name="digital">The digital array.</param>
/// <param name="sizeSignalXML">The size signal XML.</param>
/// <param name="typ">The enum-typ array.</param>
/// <returns>double as fitness</returns>
extern "C" __declspec(dllexport) double __cdecl  executeES1(int sizeAnalogXML, 
									 float analog[],
									 int sizeDigitalXML,
									 bool digital[],
									 int sizeSignalXML, 
									 enum SIGNALTYP typ[]);


/// <summary>
/// ???????????????
/// </summary>
/// <param name="sizeAnalogXML">The size analog XML.</param>
/// <param name="analog">The analog array.</param>
/// <param name="sizeDigitalXML">The size digital XML.</param>
/// <param name="digital">The digital array.</param>
/// <param name="sizeSignalXML">The size signal XML.</param>
/// <param name="typ">The enum-typ array.</param>
/// <returns>double as fitness</returns>
extern "C" __declspec(dllexport) double __cdecl  executeES2(int sizeAnalogXML, 
									 float analog[],
									 int sizeDigitalXML,
									 bool digital[],
									 int sizeSignalXML, 
									 enum SIGNALTYP typ[]);


/// <summary>
/// ????????????????????
/// </summary>
/// <param name="sizeAnalogXML">The size analog XML.</param>
/// <param name="analog">The analog array.</param>
/// <param name="sizeDigitalXML">The size digital XML.</param>
/// <param name="digital">The digital array.</param>
/// <param name="sizeSignalXML">The size signal XML.</param>
/// <param name="typ">The enum-typ array.</param>
/// <returns>double as fitness</returns>
extern "C" __declspec(dllexport) double __cdecl  executeES3(int sizeAnalogXML, 
									 float analog[],
									 int sizeDigitalXML,
									 bool digital[],
									 int sizeSignalXML, 
									 enum SIGNALTYP typ[]);




/// <summary>
/// return parameter analog[0]
/// </summary>
/// <param name="sizeAnalogXML">The size analog XML.</param>
/// <param name="analog">The analog array.</param>
/// <param name="sizeDigitalXML">The size digital XML.</param>
/// <param name="digital">The digital array.</param>
/// <param name="sizeSignalXML">The size signal XML.</param>
/// <param name="typ">The enum-typ array.</param>
/// <returns>double as fitness</returns>
extern "C" __declspec(dllexport) double __cdecl testES0(int sizeAnalogXML, 
				 float analog[],
				 int sizeDigitalXML,
				 bool digital[],
				 int sizeSignalXML, 
				 enum SIGNALTYP typ[]);


/// <summary>
/// return 0
/// </summary>
/// <param name="sizeAnalogXML">The size analog XML.</param>
/// <param name="analog">The analog array.</param>
/// <param name="sizeDigitalXML">The size digital XML.</param>
/// <param name="digital">The digital array.</param>
/// <param name="sizeSignalXML">The size signal XML.</param>
/// <param name="typ">The enum-typ array.</param>
/// <returns>double as fitness</returns>
extern "C" __declspec(dllexport) double __cdecl testES1(int sizeAnalogXML, 
				 float analog[],
				 int sizeDigitalXML,
				 bool digital[],
				 int sizeSignalXML, 
				 enum SIGNALTYP typ[]);


/// <summary>
/// return 1
/// </summary>
/// <param name="sizeAnalogXML">The size analog XML.</param>
/// <param name="analog">The analog array.</param>
/// <param name="sizeDigitalXML">The size digital XML.</param>
/// <param name="digital">The digital array.</param>
/// <param name="sizeSignalXML">The size signal XML.</param>
/// <param name="typ">The enum-typ array.</param>
/// <returns>double as fitness</returns>
extern "C" __declspec(dllexport) double __cdecl testES2(int sizeAnalogXML, 
				 float analog[],
				 int sizeDigitalXML,
				 bool digital[],
				 int sizeSignalXML, 
				 enum SIGNALTYP typ[]);

/// <summary>
/// return analog[0] + digital[0] + typ[0]
/// </summary>
/// <param name="sizeAnalogXML">The size analog XML.</param>
/// <param name="analog">The analog array.</param>
/// <param name="sizeDigitalXML">The size digital XML.</param>
/// <param name="digital">The digital array.</param>
/// <param name="sizeSignalXML">The size signal XML.</param>
/// <param name="typ">The enum-typ array.</param>
/// <returns>double as fitness</returns>
extern "C" __declspec(dllexport) double __cdecl testES3(int sizeAnalogXML, 
				 float analog[],
				 int sizeDigitalXML,
				 bool digital[],
				 int sizeSignalXML, 
				 enum SIGNALTYP typ[]);



//int main( int argc, const char* argv[] )
//{
//	executeES(L"test",0,NULL,0,NULL,0,NULL);
//	
//	return true;
//}