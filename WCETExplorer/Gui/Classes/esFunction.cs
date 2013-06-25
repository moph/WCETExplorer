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
        public EvolutionAlgo.calculateFitness_delegate f { get; set; }

        public override string ToString()
        {
            return this.name;
        }
    }
}
