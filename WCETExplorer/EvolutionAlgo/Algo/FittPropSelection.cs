using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    class FittPropSelection : SelectionStrategy
    {
        public override Generation select(Generation myGeneration)
        {
            return myGeneration;
        }
    }
}
