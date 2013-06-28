#include <CppUTest\TestHarness.h>
#include <executeES.h>
#include <functionConstraints.h>

TEST_GROUP(executeES3)
{
 float analog[10];
 bool digital[40];
 enum SIGNALTYP typ[10];

 void setup()
 {
	 /*
  for(int i=0; i < 10 ; i++)
  {
   analog[i] = 0.7f;
  }
  */
	 analog[0] = 0.6f;
	 analog[1] = 0.1f;
	 analog[2] = 0.2f;
	 analog[3] = 0.3f;
	 analog[4] = 0.4f;
	 analog[5] = 0.5f;
	 analog[6] = 0.88f;
	 analog[7] = 0.89f;
	 analog[8] = 0.90f;
	 analog[9] = 0.95f;
  for(int i=0; i < 40 ; i++)
  {
   digital[i] = true;
	if (i%2 == 0){
		digital[i] = false;
	}
  }

  for(int i=0; i < 10 ; i++)
  {
   typ[i] = SIGNALTYP::RECHTECK;
  }
 };

 void teardown()
 {
 };
};

TEST(executeES3,NORMED_CALL)
{
 double actual = executeES3(10,analog, 40, digital, 10, typ);
 DOUBLES_EQUAL(-3.0 , actual, 0.01f);
}