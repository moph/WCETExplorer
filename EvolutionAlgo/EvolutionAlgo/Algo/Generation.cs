using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    /// <summary>
    /// Author: David Schreiner
    /// </summary>
    class Generation
    {

        private int _size;

        public Generation(int size)
        {
            this._size = size;

        }

        public Genom getBestGenom()
        {
            Genom dummy = new Genom(null);
            return dummy;
        }

        public double getAverageFitness()
        {


            return 0;
        }

        public void createGenes()
        {


        }

        public void crossover()
        {


        }

        public void mutate()
        {


        }
    }
}
