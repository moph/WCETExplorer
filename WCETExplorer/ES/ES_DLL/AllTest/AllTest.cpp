// AllTest.cpp : Defines the entry point for the console application.
//
#include <CppUTest\CommandLineTestRunner.h>
#include <CppUTest\TestHarness.h>
#include <iostream>

	

int main(int argc, char* argv[])
{
	if(argc > 1)
	{
		//argv[argc]="-n";argv[argc+1]="ENUM_BEGIN_AT_ZERO";argc += 2;
	} 

	RUN_ALL_TESTS(argc, argv);
	std::cin.ignore();
	return 0;
}

