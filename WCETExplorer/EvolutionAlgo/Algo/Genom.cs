using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    /// <summary>
    /// Author:David Schreiner
    /// </summary>
    public class Genom
    {

        public Parameter _param;
        private double _fittness;
        private EvolutionAlgo _ea;

        //Create Genom and Calucalte Fitness.
        public Genom(Parameter param, EvolutionAlgo ea)
        {
            this._param = param;
            
            this._ea = ea;
         
                
            
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

        public void calculateFitnes()
        {
            this._fittness = _ea._calculateFitness(_param.analog.Length, _param.analog, _param.digital.Length, _param.digital, _param.enums.Length, _param.enums);
        }

        
    }
}
