using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    public class Fitness : StopCriterion
    {
        public double fitness { get; set; }

        public Fitness(double fitness)
        {
            this.fitness = fitness;
        }

        public override bool fulfilled()
        {
            return false;
        }
    }
}
