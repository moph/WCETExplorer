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
        private Genom[] _genomArray;
        private Parameter _blaram;

        public Generation(int size,Parameter param)
        {
            this._blaram = param;
            this._size = size;
            createGenes();
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
            Parameter genomParameter;
            Random ran = new Random();
            int countAnalog = _blaram.analog.Length;
            int countDigital = _blaram.digital.Length;
            int countEnums = _blaram.enums.Length;
            float[] analogVal = new float[countAnalog];
            for (int k = 0; k < this._size; k++)
            {
                for (int i = 0; i < countAnalog; i++)
                {

                    analogVal[i] = (float)ran.NextDouble();
                }
            }

        }

        public void crossover()
        {


        }

        public void mutate()
        {


        }
    }
}
