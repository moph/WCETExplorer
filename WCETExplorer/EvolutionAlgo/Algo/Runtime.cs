using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Author: Josué Kiefer
/// Date: 25.05.2013
/// </summary> 
namespace EvolutionAlgo
{
    public class Runtime : StopCriterion
    {
        public double runtime { get; set; }

        public Runtime(double runtime)
        {
            this.runtime = runtime;
        }

        public override bool fulfilled() { return false; }

        public bool fulfilled(double runtime)
        {
            if (runtime < this.runtime)
                return false;
            else
                return true;
        }
    }
}
