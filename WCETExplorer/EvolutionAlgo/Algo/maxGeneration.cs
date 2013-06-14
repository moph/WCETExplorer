using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    public class maxGeneration : StopCriterion
    {
        public uint maxGen { get; set; }

        public maxGeneration(uint maxGen)
        {
            this.maxGen = maxGen;
        }

        public override bool fulfilled() {
            return false;
        }
    }
}
