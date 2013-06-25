using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    /// <summary>
    /// Author: David Schreiner
    /// </summary>
    public class Generation
    {
        // 21.5.13 Note to David: Constructor of Class Genom calculates Fitness.
        EvolutionAlgo _ea;
        public uint _size;
        public Genom[] _genomArray;
        private Parameter _blaram;
        private double mutateRate;
        private uint maxCrossover;
      
        public Generation(uint size, Parameter param, double mutateRate, uint maxCrossover, EvolutionAlgo ea)
        {
            this._blaram = param;
            this._size = size;
            this.mutateRate = mutateRate;
            this.maxCrossover = maxCrossover;
            //this._genomArray[] = new Genom[size];
            this._ea = ea;
            this._genomArray = new Genom[size];
            this.createGenes(0);    
        }

        // Create new Generation but use existing genoms.
        public Generation(ArrayList gen, uint size, double mutateRate, uint maxCrossover, EvolutionAlgo ea)
        {
            
            // create (size - gen._size) new genes.
            this._genomArray = new Genom[size];
            this._ea = ea;
            this._blaram = ((Genom)gen[0])._param;
            this.mutateRate = mutateRate;
            this.maxCrossover = maxCrossover;
            this._size = size;
            //this._genomArray[] = new Genom[size];
            this._genomArray = new Genom[size];
            gen.CopyTo(this._genomArray, 0);
            this.createGenes((uint)gen.Count);
        }

        public Genom getBestGenom()
        {
            Genom dummy = this._genomArray[0];
            
            for (int k = 1; k < _genomArray.Length; k++)
            {
                if (_genomArray[k].fittness > dummy.fittness)
                {
                    dummy = _genomArray[k];
                }
            }
            return dummy;
        }

        public double getAverageFitness()
        {
            int lenght = _genomArray.Length;
            double avg = 0;
            
            for (int k = 0; k < lenght; k++)
                avg = avg + _genomArray[k].fittness;


            return (avg/lenght);
        }

        private void createGenes(uint givenGenes)
        {
            Parameter genomParameter;
            Random ran;
            //
            int countAnalog = _blaram.analog.Length;
            int countDigital = _blaram.digital.Length;
            int countEnums = _blaram.enums.Length;
            float[] analogVal = new float[countAnalog];
            bool[] digitalVal = new bool[countDigital];
            int[] enumVal = new int[1];
            if(countEnums > 0)
                enumVal = new int[countEnums];
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
                _genomArray[k] = new Genom(genomParameter, _ea);
                //Calculate Fittness.
                
            }
            
            for (int u = 0; u < _genomArray.Length; u++)
            {
                _genomArray[u].fittness = _ea._calculateFitness(_genomArray[u]._param.analog.Length, _genomArray[u]._param.analog, _genomArray[u]._param.digital.Length, _genomArray[u]._param.digital, _genomArray[u]._param.enums.Length, _genomArray[u]._param.enums);
            }
             
        }

        public void crossover()
        {
            Random rand = new Random();
           
            int abgra = rand.Next(0, _genomArray[0]._param.analog.Length-1); // Where the crossover will take place
            int abgrd = rand.Next(0, _genomArray[0]._param.digital.Length-1);
            int abgre = rand.Next(0, _genomArray[0]._param.enums.Length-1);
            int testJ;
            for (int k = 0; k < maxCrossover; k++)
            {
                Random test = new Random();
                testJ = test.Next(0, _genomArray.Length-1);
                for (; abgra < _genomArray[0]._param.analog.Length; abgra++)
                {
                    _genomArray[testJ+1]._param.analog[abgra] = _genomArray[testJ]._param.analog[abgra]; 
                }
                for (; abgrd < _genomArray[0]._param.digital.Length; abgrd++)
                {
                    _genomArray[testJ+1]._param.digital[abgrd] = _genomArray[testJ]._param.digital[abgrd];
                }
                for (; abgre < _genomArray[0]._param.enums.Length; abgre++)
                {
                    _genomArray[testJ+1]._param.enums[abgre] = _genomArray[testJ]._param.enums[abgre];
                }
            }

        }

        public void mutate()
        {
            Random rand = new Random();

            int countAnalog;
            int countDigital;
            int countEnums;
            int k;
            for (k = 0; k < this._genomArray.Length; k++)
            {
                if (rand.NextDouble() == mutateRate)
                {
                    countAnalog = this._genomArray[k]._param.analog.Length;
                    countDigital = this._genomArray[k]._param.digital.Length;
                    countEnums = this._genomArray[k]._param.enums.Length;
                    for (k = 0; k < rand.Next(countAnalog); k++)
                    {
                        this._genomArray[k]._param.analog[rand.Next(countAnalog - 1)] = (float)rand.NextDouble();
                    }

                    for (k = 0; k < rand.Next(countDigital); k++)
                    {
                        this._genomArray[k]._param.digital[rand.Next(countDigital - 1)] = rndBoolean();
                    }

                    for (k = 0; k < rand.Next(countEnums); k++)
                    {
                        this._genomArray[k]._param.enums[rand.Next(countEnums - 1)] = rand.Next(10);
                    }
                }
            }
        }
        private bool rndBoolean()
        {
            Random rand = new Random();
            if (rand.Next(0, 1) == 0)
            {

                return true;
            }
            return false;
        }


    }
}
