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
        // 21.5.13 Note to David: Constructor of Class Genom calculates Fitness.

        private uint _size;
        private Genom[] _genomArray;
        private Parameter _blaram;
        

        public Generation(uint size,Parameter param)
        {
            this._blaram = param;
            this._size = size;

            this.createGenes(0);
        }

        // Create new Generation but use existing genoms.
        public Generation(Generation gen, uint size)
        {
            // create (size - gen._size) new genes.
            createGenes(gen._size - size);
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

        private void createGenes(uint givenGenes)
        {
            Parameter genomParameter;
            Random ran;
            int countAnalog = _blaram.analog.Length;
            int countDigital = _blaram.digital.Length;
            int countEnums = _blaram.enums.Length;
            float[] analogVal = new float[countAnalog];
            bool[] digitalVal = new bool[countDigital];
            int[] enumVal = new int[countEnums];
            for (uint k = givenGenes; k < this._size; k++)
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
            Random rand = new Random();
            int countAnalog = _blaram.analog.Length;
            int countDigital = _blaram.digital.Length;
            int countEnums = _blaram.enums.Length;
            int k;
            for (k = 0; k < rand.Next(countAnalog); k++)
            {
                _blaram.analog[rand.Next(countAnalog - 1)] = (float)rand.NextDouble(); 
            }

            for (k = 0; k < rand.Next(countDigital); k++)
            {
                _blaram.digital[rand.Next(countDigital - 1)] = rndBoolean();
            }

            for (k = 0; k < rand.Next(countEnums); k++)
            {
                _blaram.enums[rand.Next(countEnums - 1)] = rand.Next(10);
            }
        }
        private bool rndBoolean()
        {
            Random rand = new Random();
            return rand.Next(0, 2) == 0;
        }


    }
}
