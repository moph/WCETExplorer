using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace EvolutionAlgo
{   
    /// <summary>
    /// Author: Andreas Engel
    /// Date: 16.05.2013
    /// </summary>
    class EvolutionAlgo
    {
        private uint _startSize;
        private AlgoSettings _aSettings;
        private Parameter _param;
        private Genom _bestGenom;
        private printResult_delegate _printResult;
        private finishedWCET_delegate _finishedWCET;
        private finishedManual_delegate _finishedManual;
        public calculateFitness_delegate _calculateFitness;
        private System.Threading.Thread _calculationThread;
        private bool _automatic;

        // Delegates for Function calls in GUI by automatic Calculation.
        private delegate void printResult_delegate(Generation myGeneration, Genom myGenom);
        private delegate void finishedWCET_delegate(Genom myGenom);

        // Delegate for Function call in GUI by manual Calculation.
        private delegate void finishedManual_delegate(Genom myGenom);

        // Delegate for Function call in DLL.
        public delegate double calculateFitness_delegate(float[] analog, bool[] digital, int[] enmus);

        // Constructor for automatic calculation of WCET. 
        EvolutionAlgo(Parameter param, AlgoSettings aSettings, printResult_delegate printResult, finishedWCET_delegate finishedWCET,
            calculateFitness_delegate calculateFitness) : this() {
            this._param = param;
            this._aSettings = aSettings;
            this._printResult = printResult;
            this._finishedWCET = finishedWCET;
            this._calculateFitness = calculateFitness;
            this._automatic = true;
            _startSize = _aSettings.populationSize;
        }
       
        // Constructor for manual calculation of WCET.
        EvolutionAlgo(Parameter param, finishedManual_delegate finishedManual, calculateFitness_delegate calculateFitness) : this()
        {
            this._param = param;
            this._finishedManual = finishedManual;
            this._calculateFitness = calculateFitness;
            this._automatic = false;
            _startSize = _aSettings.populationSize;
        }

        // Private Constructor for optional initialisation.
        private EvolutionAlgo() {
            this._calculationThread = new System.Threading.Thread(calculation);
        }

        // Starts calculation of WCET.
        public void go() {
            // Start Thread that calculates WCET.
            _calculationThread.Start();
        }

        // Stops calculation of WCET.
        public void stop() {
            // Abort Thread that calculates WCET.
            _calculationThread.Abort();
        }

        // Does calculation of WCET.
        private void calculation() {
            // Check if automatic or manual calculation.
            if (_automatic) {
                // Start loop and create new Generation.
                Generation myGeneration = new Generation(_aSettings.populationSize, _param, _aSettings.mutationRate, _aSettings.crossoverCount, this);
                _bestGenom = myGeneration.getBestGenom();
                do
                {
                    // Call Functions in GUI to show results.
                    _printResult(myGeneration, myGeneration.getBestGenom());
                    // Crossover and Mutate Generation.
                    myGeneration.crossover();
                    myGeneration.mutate();
                    // Select Genes from Generation with selected Selection Strategy & Create new Generation but use existing genoms.
                    myGeneration = new Generation(_aSettings.strategy.select(generation2Array(myGeneration)), _aSettings.populationSize, _aSettings.mutationRate, _aSettings.crossoverCount, this);
                    // Change bestGenom if necessary.
                    if (_bestGenom.fittness < myGeneration.getBestGenom().fittness) {
                        _bestGenom = myGeneration.getBestGenom();
                    }
                } while (again());
                // Finish and return Genom with WCET.
                _finishedWCET(_bestGenom);
            } 
            // Do Manual calculation.
            else {
                // Create Genom.
                Genom myGenom = new Genom(_param);
                // Return Genom to GUI.
                _finishedManual(myGenom);
            }
        }

        // Checks if Stop Criterions are fulfilled.
        private bool again() {
            // Checks all Stop Criterions in array _aSettings.stop.
            for (int i = 0; i < _aSettings.stop.Length; i++) {
                // if any is false -> return false and terminate algorithm.
                if (_aSettings.stop[i].fulfilled()) {
                    return false;
                }
            }
            return true;
        }

        private ArrayList generation2Array(Generation myGeneration) {
            ArrayList arrayGeneration = new ArrayList();
            for(int i=0;i<myGeneration._size;i++) {
                arrayGeneration.Add(myGeneration._genomArray[i]);
            }
            return arrayGeneration;
        }


        /*
         * ToDo:
         * - Maybe delte private constructor and make calculation static :/
         * - Add correct parameters to delegate calculate Fitness.
         * - Genom needs Function _calculateFitness to call DLL.
         * - Add genomeComparer to compare bestGonom tih others.
         */
    }
}
