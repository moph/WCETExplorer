using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Author: Josué Kiefer
/// Date: 17.05.2013
/// </summary>
namespace EvolutionAlgo{   
    abstract class SelectionStrategy{

        abstract public Generation select(Generation myGeneration);
    }
}
