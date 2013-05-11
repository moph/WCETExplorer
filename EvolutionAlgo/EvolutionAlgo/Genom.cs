using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    class Genom
    {

        private Parameter _param;
        private double _fittness;

        public Genom(Parameter param)
        {
            this._param = param;

        }

        public double getFittness()
        {
            return _fittness;
        }

        public void calcFitness()
        {

        }
    }
}
