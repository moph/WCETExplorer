using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace EvolutionAlgo
{
    public class TournamentSelection : SelectionStrategy
    {
        private double _newPopSize;
        private Random _random;
        private int _nrOfParticipants;
        private String _name = "TournamentSelection";

        public override String ToString()
        {
            return this._name;
        }


        public TournamentSelection()
        {
            this._newPopSize = 0.75;
            this._nrOfParticipants = 2;
            this._random = new Random();
        }

        private ArrayList selectParticipants(ArrayList l, Random random)
        {
            ArrayList participants = new ArrayList();
            int i;

            for (i = 0; i < this._nrOfParticipants; i++)
            {
                participants.Add((Genom)l[random.Next(l.Count - 1)]);
            }

            return participants;
        }

        private Genom tournamentWinner(ArrayList l)
        {
            Genom best = (Genom)l[0];
            int i;

            for (i = 1; i < l.Count; i++)
            {
                if (best.fittness < ((Genom)l[i]).fittness)
                    best = (Genom)l[i];
            }

            return best;
        }

        public override ArrayList select(ArrayList gens)
        {
            int nr, i;
            ArrayList l = new ArrayList(), participants;


            if (gens.Count >= this._newPopSize)
                nr = (int)(gens.Count * this._newPopSize);
            else
                nr = 1;

            for (i = 0; i < nr; i++)
            {
                participants = this.selectParticipants(gens, this._random);
                l.Add(tournamentWinner(participants));
            }

            return l;
        }
    }
}