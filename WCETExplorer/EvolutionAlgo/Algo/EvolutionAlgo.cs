using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace EvolutionAlgo
{   
    /// <summary>
    /// Author: Andreas Engel
    /// Date: 16.05.2013
    /// </summary>
    class EvolutionAlgo
    {
        protected uint _startSize;
        private AlgoSettings _aSettings;
        private Parameter _param;
        private Genom _bestGenom;
        private printResult_delegate _printResult;
        private finishedWCET_delegate _finishedWCET;
        private finishedManual_delegate _finishedManual;
        private calculateFitness_delegate _calculateFitness;
        System.Threading.Thread _calculationThread;

        // Delegates for Function calls in GUI by automatic Calculation.
        private delegate void printResult_delegate(Generation myGeneration, Genom myGenom);
        private delegate void finishedWCET_delegate(Genom myGenom);

        // Delegate for Function call in GUI by manual Calculation.
        private delegate void finishedManual_delegate(Genom myGenom);

        // Delegate for Function call in DLL.
        private delegate double calculateFitness_delegate(float[] analog, bool[] digital, int[] enmus);

        // Constructor for automatic calculation of WCET. 
        EvolutionAlgo(Parameter param, AlgoSettings aSettings, printResult_delegate printResult, finishedWCET_delegate finishedWCET,
            calculateFitness_delegate calculateFitness) : this() {
            this._param = param;
            this._aSettings = aSettings;
            this._printResult = printResult;
            this._finishedWCET = finishedWCET;
            this._calculateFitness = calculateFitness;
        }
       
        // Constructor for manual calculation of WCET.
        EvolutionAlgo(Parameter param, finishedManual_delegate finishedManual, calculateFitness_delegate calculateFitness) : this()
        {
            this._param = param;
            this._finishedManual = finishedManual;
            this._calculateFitness = calculateFitness;
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

        }


        /*
         * ToDo:
         * - Maybe delte private constructor and make calculation static :/
         * - Add correct parameters to delegate calculate Fitness.
         */
    }
}
