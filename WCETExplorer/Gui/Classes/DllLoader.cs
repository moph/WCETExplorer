using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Gui.Classes
{
    class DllLoader
    {
        private static string xsdPath = "es_config.xsd";

        private Dictionary<string, esFunction> functions = new Dictionary<string,esFunction>();
        private es theEs;
        private bool success;
        private IntPtr hModule = IntPtr.Zero;


        public string[] loadDll(string file){
            if (IntPtr.Zero!= hModule)
            {
                unloadDll();
            }
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, xsdPath);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += new System.Xml.Schema.ValidationEventHandler(settings_ValidationEventHandler);
            using (XmlReader reader = XmlReader.Create(file, settings))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(es));
                success = true;
                theEs = serializer.Deserialize(reader) as es;

                if (!success)
                {
                    throw new ArgumentException("Something was wrong with the supplied file / filename: " + file);
                }

                hModule = LoadLibrary(theEs.dll);
                if (IntPtr.Zero == hModule)
                {
                    Console.WriteLine("Error: " + Marshal.GetLastWin32Error().ToString());
                    return null;
                }
                string[] ret = new string[theEs.function.Length];
                for (int i = 0; i < theEs.function.Length; i++)
                {
                    string name = theEs.function[i].name;
                    esFunction function = theEs.function[i];
                    ret[i] = name;
                    functions.Add(name, function);

                    IntPtr pPtr = GetProcAddress(hModule, name);
                    if (IntPtr.Zero == pPtr)
                    {
                        throw new Exception("Procedure " + name + " doesn't exist.");
                    }
                    function.f = (esFunction.executeEs) Marshal.GetDelegateForFunctionPointer(pPtr, typeof(esFunction.executeEs));
                }


                return ret;
            }
        }

        public esFunction loadFunction(string name)
        {
            esFunction ret;
            if (!functions.TryGetValue(name, out ret))
            {
                throw new ArgumentException("Function " + name + " doesn't exist.");
            }
            return ret;
        }

        public void unloadDll()
        {
            FreeLibrary(hModule);
            hModule = IntPtr.Zero;
        }

        private void settings_ValidationEventHandler(Object sender, ValidationEventArgs e)
        {
            success = false;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32")]
        public extern static IntPtr LoadLibrary(string lpLibFileName);

        [DllImport("kernel32")]
        public extern static Int32 FreeLibrary(IntPtr hModule);



        private void quickTest()
        {
            string[] fNames = loadDll("Es_config.xml");
            foreach (string name in fNames)
            {
                esFunction func = loadFunction(name);
                float[] bla = new float[1];
                bla[0] = 0.5f;
                Console.WriteLine(func.f(1, bla, 0, null, 0, null));
            }
        }

        static void Main()
        {
            new DllLoader().quickTest();
        }
    }
}
