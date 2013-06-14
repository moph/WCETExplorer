using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    /// <summary>
    /// Author: David Schreiner
    /// </summary>
    public class Parameter
    {
        public float[] analog;
        public bool[] digital;
        public int[] enums;
        
        
        public Parameter(float[] analog, bool[] digital,int[] enums) {
            this.analog = analog;
            this.digital = digital;
            this.enums = enums;
            


            
        }
        
    }
}
