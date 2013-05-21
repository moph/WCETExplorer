using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EvolutionAlgo
{
    
    abstract class StopCriterion
    {
        // Returns true if Stop Criterion is reached.
        public abstract bool fulfilled();
    }
}
