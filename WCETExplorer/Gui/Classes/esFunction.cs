using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Xml.Serialization;

/// Author: Maximilian Krög
namespace Gui.Classes
{
    partial class esFunction
    {
        /// <summary>
        /// The the ES-function
        /// </summary>
        [XmlIgnore]
        public executeEs f { get; set; }

        /// <summary>
        /// Calls the underlying ES which will return a fitness value or in case of error:
        /// ERROR_CODE_USING -1.0 Parameteranzahl stimmt nicht mit Definition überein.
        /// ERROR_CODE_INDEX_OUT_OF_BOUND -2.0 Zugriff auf ein Element ausserhalb des addressierten Bereichs.
        /// ERROR_CODE_NOT_NORMED -3.0 Wert wurde nicht normiert übergeben.
        /// ERROR_CODE_MIN_MAX_DISTORTED -4.0 Min / Max Definition fehlerhaft.
        /// </summary>
        /// <param name="sizeAnalogXML">Number of analog (float) parameters.</param>
        /// <param name="analog">Array of float.</param>
        /// <param name="sizeDigitalXML">Number of digital (bool) parameters.</param>
        /// <param name="digital">Array of bool.</param>
        /// <param name="sizeSignalXML">Number of enum parameters.</param>
        /// <param name="typ">Array of int.</param>
        /// <returns>The fitness of the current Parameter set.</returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate double executeEs(int sizeAnalogXML, float[] analog, int sizeDigitalXML, bool[] digital, int sizeSignalXML, int[] typ);

        private esFunction()
        {
            enumField = new esFunctionEnum[0];
            binaryField = new esFunctionBinary[0];
            floatField = new esFunctionFloat[0];
        }

        public override string ToString()
        {
            return name;
        }
    }
}
