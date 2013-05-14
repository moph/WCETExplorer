using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    class Parameter
    {
        public float[] analog;
        public bool[] digital;
        public int[] enums;
        
        public Parameter(float[] analog, bool[] digital,int[] enums) {
            this.analog = analog;
            this.digital = digital;
            this.enums = enums;
            //Dies ist ein GitTest
        }
        
    }
}
