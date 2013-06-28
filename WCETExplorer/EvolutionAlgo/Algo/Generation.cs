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
        private int generationCount;
        private Random rand;
        private Random rand2;
      
        public Generation(uint size, Parameter param, double mutateRate, uint maxCrossover, EvolutionAlgo ea)
        {
            this.rand = new Random();
            this.rand2 = new Random();
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
            this.rand = new Random();
            this.rand2 = new Random();
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
                for (int i = 0; i < countAnalog; i++) // Erzeugung Random Analogwerte
                {
                    analogVal[i] = (float)this.rand.NextDouble();
                }
                for (int i = 0; i < countDigital; i++) // Erzeugung Random Digitalwerte
                {
                    digitalVal[i] = rndBoolean();
                }
                for (int i = 0; i < countEnums; i++) // Erzeugung Random Enum werte
                {
                    enumVal[i] = this.rand.Next(10);
                }
                
                genomParameter = new Parameter(analogVal, digitalVal, enumVal); //Parameter und Genomerzeugung
                _genomArray[k] = new Genom(genomParameter, _ea);
                //Calculate Fittness.
                
            }
        }

        public void crossover()
        {
            int abgra = this.rand.Next(0, _genomArray[0]._param.analog.Length); // Where the crossover will take place
            int abgrd = this.rand.Next(0, _genomArray[0]._param.digital.Length);
            int abgre = this.rand.Next(0, _genomArray[0]._param.enums.Length);
            int testJ;
            for (int k = 0; k < maxCrossover; k++)
            {
                testJ = this.rand2.Next(0, _genomArray.Length);
                for (; abgra < _genomArray[0]._param.analog.Length; abgra++)
                {
                    if (testJ + 1 == _genomArray.Length)
                        testJ = 0;

                    _genomArray[testJ + 1]._param.analog[abgra] = _genomArray[testJ]._param.analog[abgra]; 
                }
                for (; abgrd < _genomArray[0]._param.digital.Length; abgrd++)
                {
                    if (testJ + 1 == _genomArray.Length)
                        testJ = 0;

                    _genomArray[testJ + 1]._param.digital[abgrd] = _genomArray[testJ]._param.digital[abgrd];
                }
                for (; abgre < _genomArray[0]._param.enums.Length; abgre++)
                {
                    if (testJ + 1 == _genomArray.Length)
                        testJ = 0;

                    _genomArray[testJ + 1]._param.enums[abgre] = _genomArray[testJ]._param.enums[abgre];
                }
            }

        }

        public void mutate()
        {
            int countAnalog;
            int countDigital;
            int countEnums;
            int k;
            for (k = 0; k < this._genomArray.Length; k++)
            {
                if (this.rand.Next(101) <= ((int)(mutateRate * 100)))
                {
                    countAnalog = this._genomArray[k]._param.analog.Length;
                    countDigital = this._genomArray[k]._param.digital.Length;
                    countEnums = this._genomArray[k]._param.enums.Length;
                    for (k = 0; k < this.rand.Next(countAnalog); k++)
                    {
                        this._genomArray[k]._param.analog[this.rand.Next(countAnalog)] = (float)this.rand.NextDouble();
                    }

                    for (k = 0; k < this.rand.Next(countDigital); k++)
                    {
                        this._genomArray[k]._param.digital[this.rand.Next(countDigital)] = rndBoolean();
                    }

                    for (k = 0; k < this.rand.Next(countEnums); k++)
                    {
                        this._genomArray[k]._param.enums[this.rand.Next(countEnums)] = this.rand.Next(10);
                    }
                }
            }
        }
        private bool rndBoolean()
        {
            if (this.rand.Next(0, 2) == 0)
            {
                return true;
            }

            return false;
        }


    }
} 
