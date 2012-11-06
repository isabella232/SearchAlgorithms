using System;
using System.Linq;
using System.Collections.Generic;

namespace SearchAlgorithms.Automata
{
    public class Nfa
    {
        private readonly Tuple<int, int> _startState;
        private readonly Dictionary<Tuple<int, int>, Dictionary<char, AutomataState>> _transitions;
        private readonly AutomataState _finalStates;

        public Nfa()
        {
            _startState = Tuple.Create(0, 0);
            _transitions = new Dictionary<Tuple<int, int>, Dictionary<char, AutomataState>>();
            _finalStates = new AutomataState();
        }

        public AutomataState StartState
        {
            get { return _Expand(new AutomataState(_startState)); }
        }

        public Dictionary<Tuple<int, int>, Dictionary<char, AutomataState>> Transitions
        {
            get { return _transitions; }
        }

        public AutomataState FinalStates
        {
            get { return _finalStates; }
        }

        public static char Any
        {
            get { return 'Ä'; }
        }

        public static char Epsilon
        {
            get { return 'Ë'; }
        }

        public void AddTransition(Tuple<int,int> src, char input, Tuple<int,int> dest)
        {
            Dictionary<char, AutomataState> set;
            if (_transitions.TryGetValue(src, out set))
            {
                if (set.ContainsKey(input))
                    set[input].Add(dest);
                else
                    set.Add(input, new AutomataState(dest));
            }

            else
            {
                _transitions.Add(src, new Dictionary<char, AutomataState>
                    {
                        {input, new AutomataState (dest)}
                    });
            }

        }

        public void AddFinalState(Tuple<int, int> finalState)
        {
            _finalStates.Add(finalState);
        }

        public string WriteGraph()
        {
            string dotfile = "digraph nfa {\nrankdir=LR;\nnode [shape = circle];\n";

            foreach (var transition in _transitions)
            {
                string startState = "N" + transition.Key.Item1 + "_" + transition.Key.Item2 + " -> ";

                foreach (KeyValuePair<char, AutomataState> state in transition.Value)
                {
                    foreach (var tuple in state.Value.State)
                    {
                        string destState = "N" + tuple.Item1 + "_" + tuple.Item2; 
                        dotfile += startState + destState + " [ label = \"" + state.Key + "\" ];\n";
                    }
                }
            }

            dotfile += "}";
            return dotfile;
        }

        public Dfa ConstructDfaUsingPowerSet()
        {
            Dfa dfa = new Dfa(StartState);
            Stack<AutomataState> frontier = new Stack<AutomataState>();
            frontier.Push(StartState);
            HashSet<AutomataState> seen = new HashSet<AutomataState>();

            while (frontier.Count > 0)
            {
                AutomataState state = frontier.Pop();

                IEnumerable<char> inputs = _GetInputs(state);
                foreach (char input in inputs)
                {
                    if (input.Equals(Epsilon)) continue;
                    AutomataState nextState = _NextState(state, input);

                    if (!seen.Contains(nextState))
                    {
                        frontier.Push(nextState);
                        seen.Add(nextState);
                        if (_IsFinal(nextState))
                            dfa.AddFinalState(nextState);
                    }

                    if (input == Any)
                    {
                        if (!dfa.HasTransition(state, input, nextState))
                            dfa.SetDefaultTransition(state, nextState);
                    }
                    else
                    {
                        if (!dfa.HasDefaultTransition(state, nextState))
                            dfa.AddTransition(state, input, nextState);
                    }
                }
            }

            return dfa;
        }

        private AutomataState _Expand(AutomataState states)
        {
            List<Tuple<int, int>> frontier = new List<Tuple<int, int>>();
            frontier.AddRange(states.State);

            while (frontier.Count > 0)
            {
                Tuple<int, int> state = frontier[frontier.Count - 1];
                frontier.RemoveAt(frontier.Count - 1);

                Dictionary<char, AutomataState> set;
                if (_transitions.TryGetValue(state, out set))
                {
                    AutomataState innerSet;
                    if (set.TryGetValue(Epsilon, out innerSet))
                    {
                        IList<Tuple<int, int>> newStates = innerSet.State.Except(states.State).ToList();
                        frontier.AddRange(newStates);
                        foreach (var entry in newStates)
                            states.Add(entry);
                    }
                }
            }

            return states;
        }

        private bool _IsFinal(AutomataState states)
        {
            return _finalStates.State.Intersect(states.State).Any();
        }

        private AutomataState _NextState(AutomataState states, char input)
        {
            AutomataState deststates = new AutomataState();
            foreach (Tuple<int, int> state in states.State)
            {
                Dictionary<char, AutomataState> stateTransitions;
                if (!_transitions.TryGetValue(state, out stateTransitions)) continue;

                AutomataState set;
                if (stateTransitions.TryGetValue(input, out set))
                {
                    foreach (var item in set.State)
                        deststates.Add(item);
                }

                if (!stateTransitions.TryGetValue(Any, out set)) continue;

                foreach (var item in set.State)
                    deststates.Add(item);
            }

            return _Expand(deststates);
        }

        private IEnumerable<char> _GetInputs(AutomataState states)
        {
            HashSet<char> inputs = new HashSet<char>();

            foreach (Tuple<int, int> state in states.State)
            {
                Dictionary<char, AutomataState> set;
                if (!_transitions.TryGetValue(state, out set)) continue;

                foreach (char key in set.Keys)
                    inputs.Add(key);
            }

            return inputs;
        }
    }
}
