using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    public class Runtime : StopCriterion
    {
        public double runtime { get; set; }

        public Runtime(double runtime)
        {
            this.runtime = runtime;
        }

        public override bool fulfilled()
        {
            return false;
        }

       
    }
}
