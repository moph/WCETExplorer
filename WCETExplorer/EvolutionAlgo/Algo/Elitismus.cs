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
    public class Elitismus : SelectionStrategy
    {
        private double _newPopSize;
        private String name = "Elitismus";

        public String toString(){
            return this.name;
        }

        public Elitismus()
        {
            this._newPopSize = 0.75;
        }

        public override ArrayList select(ArrayList gens)
        {
            int nr;

            if (gens.Count >= this._newPopSize)
                nr = (int)(gens.Count * this._newPopSize);
            else
                nr = 1;

            gens.Sort((IComparer)new GenomeComparer());

            return gens.GetRange(0, nr);
        }
    }
}