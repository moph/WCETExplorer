using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    class AlgoSettings
    {

        public SelectionStrategy strategy;
        public StopCriterion[] stop;
        public uint populationSize;
        public uint crossoverCount;
        public float mutationRate;
        // Dies ist kein Kommentar
        public AlgoSettings() { }

    }
}
