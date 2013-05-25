using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

/// <summary>
/// Author: Josué Kiefer
/// Date: 17.05.2013
/// </summary>
namespace EvolutionAlgo{
    abstract class SelectionStrategy
    {
        public abstract ArrayList select(ArrayList gens);
    }
}
