using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;


namespace EvolutionAlgo
{

    /// <summary>
    /// Calls the underlying ES which will return a fitness value or in case of error:
    /// ERROR_CODE_USING -1.0 Parameteranzahl stimmt nicht mit Definition überein.
    /// ERROR_CODE_INDEX_OUT_OF_BOUND -2.0 Zugriff auf ein Element ausserhalb des addressierten Bereichs.
    /// ERROR_CODE_NOT_NORMED -3.0 Wert wurde nicht normiert übergeben.
    /// ERROR_CODE_MIN_MAX_DISTORTED -4.0 Min / Max Definition fehlerhaft.
    /// </summary>
    /// <param name="sizeAnalogXML">Number of analog (float) parameters.</param>
    /// <param name="analog">Array of float.</param>
    /// <param name="sizeDigitalXML">Number of digital (bool) parameters.</param>
    /// <param name="digital">Array of bool.</param>
    /// <param name="sizeSignalXML">Number of enum parameters.</param>
    /// <param name="typ">Array of int.</param>
    /// <returns>The fitness of the current Parameter set.</returns>
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate double calculateFitness_delegate(int sizeAnalogXML, float[] analog, int sizeDigitalXML, bool[] digital, int sizeSignalXML, int[] typ);


    // Delegate for Function call in GUI by manual Calculation.
    public delegate void finishedManual_delegate(Genom myGenom);
    // Delegates for Function calls in GUI by automatic Calculation.
    public delegate void printResult_delegate(Generation myGeneration, Genom myGenom);
    public delegate void finishedWCET_delegate(Genom myGenom);
    
    /// <summary>
    /// Author: Andreas Engel
    /// Date: 16.05.2013
    /// </summary>
    public class EvolutionAlgo
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

        


        // Constructor for automatic calculation of WCET. 
        public EvolutionAlgo(Parameter param, AlgoSettings aSettings, printResult_delegate printResult, finishedWCET_delegate finishedWCET,
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
        public EvolutionAlgo(Parameter param, finishedManual_delegate finishedManual, calculateFitness_delegate calculateFitness)
            : this()
        {
            this._param = param;
            this._finishedManual = finishedManual;
            this._calculateFitness = calculateFitness;
            this._automatic = false;
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

                _printResult(myGeneration, _bestGenom);
                // Finish and return Genom with WCET.
                _finishedWCET(brutforce(_bestGenom));
            } 
            // Do Manual calculation.
            //Andi ist doof :)
            else {

                // Create Genom.
                Genom myGenom = new Genom(_param, this);
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

        private Genom brutforce(Genom myGenom) {

            //Check  analog array.
            for (int i = 0; i < myGenom._param.analog.Length; i++)
            {
                float analogValue = myGenom._param.analog[i];
                double beforeFittnes = _calculateFitness(myGenom._param.analog.Length, myGenom._param.analog, myGenom._param.digital.Length, myGenom._param.digital, myGenom._param.enums.Length, myGenom._param.enums);
                myGenom._param.analog[i] = 0;
                double afterFittnes = _calculateFitness(myGenom._param.analog.Length, myGenom._param.analog, myGenom._param.digital.Length, myGenom._param.digital, myGenom._param.enums.Length, myGenom._param.enums);

                if (beforeFittnes != afterFittnes)
                {
                    myGenom._param.analog[i] = analogValue;
                }
            }

            //Check digital array.
            for (int i = 0; i < myGenom._param.digital.Length; i++)
            {
                bool digitalValue = myGenom._param.digital[i];
                double beforeFittnes = _calculateFitness(myGenom._param.analog.Length, myGenom._param.analog, myGenom._param.digital.Length, myGenom._param.digital, myGenom._param.enums.Length, myGenom._param.enums);
                myGenom._param.digital[i] = false;
                double afterFittnes = _calculateFitness(myGenom._param.analog.Length, myGenom._param.analog, myGenom._param.digital.Length, myGenom._param.digital, myGenom._param.enums.Length, myGenom._param.enums);

                if (beforeFittnes != afterFittnes)
                {
                    myGenom._param.digital[i] = digitalValue;
                }
            }
            return myGenom;
        }
    }
}
