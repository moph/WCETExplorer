using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gui.Classes;

namespace Gui.Test
{
    class DllLoaderTest
    {
        /// <summary>
        /// Simple test to see if the call to the dll fails or not. (It works!)
        /// </summary>
        private static void quickTest()
        {
            DllLoader testee = new DllLoader();
            string[] fNames = testee.loadDll("Es_config.xml");
            foreach (string name in fNames)
            {
                esFunction func = testee.loadFunction(name);
                float[] bla = new float[func.floats.Length];
                bool[] blub = new bool[func.binaries.Length];
                int[] blurp = new int[func.enums.Length];
                Console.WriteLine(name + ": " + func.f(bla.Length, bla, blub.Length, blub, blurp.Length, blurp));
            }
        }

        /// <summary>
        /// Starts the test...
        /// </summary>
        static void Main()
        {
            //if (!AttachConsole(-1))
            //{ // Attach to an parent process console
            //    AllocConsole(); // Alloc a new console
            //}
            //Console.Read();
            try
            {
                quickTest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //[System.Runtime.InteropServices.DllImport("kernel32.dll")]
        //private extern static bool AllocConsole();

        //[System.Runtime.InteropServices.DllImport("kernel32.dll")]
        //private extern static bool AttachConsole(int pid);

    }
}
