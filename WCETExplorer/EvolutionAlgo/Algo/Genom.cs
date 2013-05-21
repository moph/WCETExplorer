using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    /// <summary>
    /// Author:David Schreiner
    /// </summary>
    class Genom
    {

        private Parameter _param;
        private double _fittness;

        //Create Genom and Calucalte Fitness.
        public Genom(Parameter param)
        {
            this._param = param;
            this.calcFitness();
        }
        public double fittness
        {
            get{
                return _fittness;
            }
            set {
                _fittness = value;
            }
        }

        public void calcFitness()
        {

        }
    }
}
