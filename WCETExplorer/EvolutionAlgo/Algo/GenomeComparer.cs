using System;
using System.Collections;
using System.Linq;
using System.Text;


/// <summary>
/// Author: Josué Kiefer
/// Date: 24.05.2013
/// </summary>
namespace EvolutionAlgo
{
    class GenomeComparer : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            Genom g1 = (Genom)a;
            Genom g2 = (Genom)b;

            if (g1.fittness < g2.fittness)
                return 1;
            if (g1.fittness > g2.fittness)
                return -1;
            else
                return 0;
        }
    }
}
