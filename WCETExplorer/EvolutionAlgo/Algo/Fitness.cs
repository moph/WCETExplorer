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
    public class Fitness : StopCriterion
    {
        private double fitness { get; set; }

        public Fitness(double fitness)
        {
            this.fitness = fitness;
        }

        public override bool fulfilled() { return false; }

        public bool fulfilled(double fitness)
        {
            if (fitness < this.fitness)
                return false;
            else
                return true;
        }
    }
}
