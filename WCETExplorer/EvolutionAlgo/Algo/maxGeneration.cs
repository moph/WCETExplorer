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
    public class maxGeneration : StopCriterion
    {
        private uint maxGen { get; set; }

        public maxGeneration(uint maxGen)
        {
            this.maxGen = maxGen;
        }

        public override bool fulfilled() { return false; }

        public bool fulfilled(uint gen)
        {
            if (gen < this.maxGen)
                return false;
            else
                return true;
        }
    }
}
