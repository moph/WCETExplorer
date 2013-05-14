using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    class maxGeneration : StopCriterion
    {
        private uint maxGen;

        public maxGeneration(uint maxGen)
        {
            this.maxGen = maxGen;
        }
    }
}
