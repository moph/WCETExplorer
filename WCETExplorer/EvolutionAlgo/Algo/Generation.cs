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

            this.createGenes();
        }

        public Genom getBestGenom()
        {
            Genom dummy = new Genom(_blaram);
            return dummy;
        }

        public double getAverageFitness()
        {
            int lenght = _genomArray.Length;
            double avg = 0;
            for (int k = 0; k < lenght; k++)
                _genomArray[k].calcFitness();

            for (int k = 0; k < lenght; k++)
                avg = avg + _genomArray[k].fittness;


            return avg/lenght;
        }

        public void createGenes()
        {
            Parameter genomParameter;
            Random ran;
            int countAnalog = _blaram.analog.Length;
            int countDigital = _blaram.digital.Length;
            int countEnums = _blaram.enums.Length;
            float[] analogVal = new float[countAnalog];
            bool[] digitalVal = new bool[countDigital];
            int[] enumVal = new int[countEnums];
            for (int k = 0; k < this._size; k++)
            {
                ran = new Random();
                for (int i = 0; i < countAnalog; i++) // Erzeugung Random Analogwerte
                {

                    analogVal[i] = (float)ran.NextDouble();
                }
                for (int i = 0; i < countDigital; i++) // Erzeugung Random Digitalwerte
                {
                    digitalVal[i] = rndBoolean();
                }
                for (int i = 0; i < countEnums; i++) // Erzeugung Random Enum werte
                {
                    enumVal[i] = ran.Next(10);
                }
                genomParameter = new Parameter(analogVal, digitalVal, enumVal); //Parameter und Genomerzeugung
                _genomArray[k] = new Genom(genomParameter);
            }

        }

        public void crossover()
        {

            Genom test = new Genom(null);
            
        }

        public void mutate()
        {


        }
        private bool rndBoolean()
        {
            Random rand = new Random();
            return rand.Next(0, 2) == 0;
        }
    }
}
