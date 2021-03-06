﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

/// <summary>
/// Author: Josué Kiefer
/// Date: 24.05.2013
/// </summary>
namespace EvolutionAlgo
{
    struct RangSelectionStruct
    {
        public Genom gen;
        public double p;
    }

    public class RangSelection : SelectionStrategy
    {
        private double _EMax;
        private double _EMin;
        private double _newPopSize;
        private Random _random;
        private String _name = "RangSelection";

        public override String ToString()
        {
            return this._name;
        }


        public RangSelection()
        {
            this._EMax = 1.9;
            this._EMin = 2.0 - this._EMax;
            this._random = new Random();
            this._newPopSize = 0.75;
        }

        private int roulette(ArrayList l, double nr)
        {
            double sum = 0;
            int index = 0;

            while ((sum < nr) && (index < l.Count))
            {
                sum += ((RangSelectionStruct)l[index++]).p;
            }

            if (index == 0)
                index++;

            return (--index);
        }

        public override ArrayList select(ArrayList gens)
        {
            int nr, i;
            RangSelectionStruct tempStruct;
            ArrayList temp = new ArrayList(), sel = new ArrayList();


            if (gens.Count >= this._newPopSize)
                nr = (int)(gens.Count * this._newPopSize);
            else
                nr = 1;

            gens.Sort((IComparer)new GenomeComparer());

            for (i = 0; i < gens.Count; i++)
            {
                tempStruct = new RangSelectionStruct();
                tempStruct.gen = (Genom)gens[i];
                tempStruct.p = (1.0 / (double)gens.Count) * (this._EMax - (this._EMax - this._EMin) * (((double)i) / ((double)gens.Count - 1.0)));
                temp.Add(tempStruct);
            }

            for (i = 0; i < nr; i++)
            {
                sel.Add(((RangSelectionStruct)temp[this.roulette(temp, this._random.NextDouble())]).gen);
            }

            return sel;
        }
    }
}
