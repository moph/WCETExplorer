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
        public double maxGen { get; set; }

        public maxGeneration(double maxGen)
        {
            this.maxGen = maxGen;
        }

        public override bool fulfilled(double gen)
        {
            if (gen < this.maxGen)
                return false;
            else
                return true;
        }
    }
}
