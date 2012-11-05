using System;
using System.Linq;

namespace SearchAlgorithms.Automata
{
    public class LevenshteinAutomata
    {
        private readonly string _term;
        private readonly int _k;

        public LevenshteinAutomata(string term, int k)
        {
            _term = term;
            _k = k;
        }

        public Nfa Construct()
        {
            Nfa nfa = new Nfa();
            int i = 0;

            foreach (char c in _term)
            {
                foreach (int e in Enumerable.Range(0, _k + 1))
                {
                    nfa.AddTransition(Tuple.Create(i, e), c, Tuple.Create(i + 1, e));

                    if (e >= _k) continue;

                    // Deletion
                    nfa.AddTransition(Tuple.Create(i, e), Nfa.Any, Tuple.Create(i, e + 1));
                    // Insertion
                    nfa.AddTransition(Tuple.Create(i, e), Nfa.Epsilon, Tuple.Create(i + 1, e + 1));
                    // Substition
                    nfa.AddTransition(Tuple.Create(i, e), Nfa.Any, Tuple.Create(i + 1, e + 1));
                }

                i++;
            }

            foreach (var e in Enumerable.Range(0, _k + 1))
            {
                if (e < _k)
                    nfa.AddTransition(Tuple.Create(_term.Length, e), Nfa.Any, Tuple.Create(_term.Length, e + 1));
                nfa.AddFinalState(Tuple.Create(_term.Length, e));
            }

            return nfa;
        }
    }
}
