using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Gui.Classes
{
    partial class esFunction
    {
        [XmlIgnore]
        public executeEs f { get; set; }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate double executeEs(int sizeAnalogXML, float[] analog, int sizeDigitalXML, bool[] digital, int sizeSignalXML, int[] typ);

        private esFunction()
        {
            enumField = new esFunctionEnum[0];
            binaryField = new esFunctionBinary[0];
            floatField = new esFunctionFloat[0];
        }

    }
}
