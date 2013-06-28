using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

/// <summary>
/// Author: Josué Kiefer
/// Date: 25.05.2013
/// </summary>
namespace EvolutionAlgo
{
    struct FittPropSelectionStruct
    {
        public Genom gen;
        public double p;
    }

    public class FittPropSelection : SelectionStrategy
    {
        private double _newPopSize;
        private Random _random;
        private String _name = "FittPropSelection";

        public override String ToString()
        {
            return this._name;
        }

        public FittPropSelection()
        {
            this._newPopSize = 0.75;
            this._random = new Random();
        }

        private int roulette(ArrayList l, double nr)
        {
            double sum = 0;
            int index = 0;

            while ((sum < nr) && (index < l.Count))
            {
                sum += ((FittPropSelectionStruct)l[index++]).p;
            }

            if (index == 0)
                index++;

            return (--index);
        }

        public override ArrayList select(ArrayList gens)
        {
            int nr, i;
            double sum = 0.0;
            FittPropSelectionStruct tempStruct;
            ArrayList sel = new ArrayList(), temp = new ArrayList();


            if ((this._newPopSize * gens.Count) > 0)
                nr = (int)(gens.Count * this._newPopSize);
            else
                nr = 1;

            foreach (Genom g in gens)
            {
                sum += g.fittness;
            }

            foreach (Genom g in gens)
            {
                tempStruct = new FittPropSelectionStruct();
                tempStruct.gen = g;
                tempStruct.p = g.fittness / sum;
                temp.Add(tempStruct);
            }

            for (i = 0; i < nr; i++)
            {
                sel.Add(((FittPropSelectionStruct)temp[this.roulette(temp, this._random.NextDouble())]).gen);
            }

            return sel;
        }
    }
}
