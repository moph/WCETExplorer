using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace EvolutionAlgo.Algo
{
    class GenomeComparer : IComparer
    {
        int IComparer.Compare(object a, object b)
        {
            Genom g1 = (Genom)a;
            Genom g2 = (Genom)b;

            if (g1.fittness > g2.fittness)
                return 1;
            if (g1.fittness < g2.fittness)
                return -1;
            else
                return 0;
        }
    }
}
