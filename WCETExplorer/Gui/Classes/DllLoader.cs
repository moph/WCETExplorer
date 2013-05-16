using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


/// Author: Maximilian Krög

namespace Gui.Classes
{
    /// <summary>
    /// Loads the xml file describing an ES and after that also loads the dll.
    /// </summary>
    class DllLoader
    {
        private static string xsdPath = "es_config.xsd";

        private Dictionary<string, esFunction> functions = new Dictionary<string, esFunction>();
        private es theEs;
        private bool success;
        private IntPtr hModule = IntPtr.Zero;

        /// <summary>
        /// Parses the xml and stores every function contained inside for later usage.
        /// </summary>
        /// <param name="file">The name of the ES-config-xml.</param>
        /// <returns>A list of function names available for this ES.</returns>
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
                    throw new Exception("Error: " + Marshal.GetLastWin32Error().ToString());
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

        /// <summary>
        /// Get the Function with the supplied name.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>The esFunction object</returns>
        /// <exception cref="System.ArgumentException">Thrown for an invalid name.</exception>
        /// <exception cref="System.NullReferenceException">Thrown when name is null.</exception>
        public esFunction loadFunction(string name)
        {
            if (null == name)
            {
                throw new NullReferenceException("Name must not be null.");
            }
            esFunction func;
            if (!functions.TryGetValue(name, out func))
            {
                throw new ArgumentException("Function " + name + " doesn't exist.");
            }
            return func;
        }

        /// <summary>
        /// May be called to unload the currently loaded dll.
        /// </summary>
        public void unloadDll()
        {
            if (IntPtr.Zero == hModule)
            {
                return;
            }
            FreeLibrary(hModule);
            hModule = IntPtr.Zero;
        }

        /// <summary>
        /// Sets success to false when an xsd violation happens.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void settings_ValidationEventHandler(Object sender, ValidationEventArgs e)
        {
            success = false;
        }

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public extern static IntPtr GetProcAddress(IntPtr hModule, string procName);

        [DllImport("kernel32.dll")]
        public extern static IntPtr LoadLibrary(string lpLibFileName);

        [DllImport("kernel32.dll")]
        public extern static Int32 FreeLibrary(IntPtr hModule);


        /// <summary>
        /// Simple test to see if the call to the dll fails or not. (It works!)
        /// </summary>
        private void quickTest()
        {
            string[] fNames = loadDll("Es_config.xml");
            foreach (string name in fNames)
            {
                esFunction func = loadFunction(name);
                float[] bla = new float[func.@float.Length];
                bool[] blub = new bool[func.binary.Length];
                int[] blurp = new int[func.@enum.Length];
                Console.WriteLine(name + ": " + func.f(bla.Length, bla, blub.Length, blub, blurp.Length, blurp));
            }
        }

        /// <summary>
        /// Ignore this
        /// </summary>
        static void Main()
        {
            new DllLoader().quickTest();
        }
    }
}
