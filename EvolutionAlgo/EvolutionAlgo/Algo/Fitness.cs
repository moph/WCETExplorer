using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    class Fitness : StopCriterion
    {
        private double fitness;

        public Fitness(double fitness)
        {
            this.fitness = fitness;
        }
    }
}
