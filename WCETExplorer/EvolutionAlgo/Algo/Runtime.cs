using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    class Runtime : StopCriterion
    {
        private double runtime;

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
