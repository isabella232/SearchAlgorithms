using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchAlgorithms.Automata
{
    public class Dfa
    {
        private readonly AutomataState _startState;
        private readonly Dictionary<AutomataState, Dictionary<char, AutomataState>> _transitions;
        private readonly Dictionary<AutomataState, AutomataState> _defaults;
        private readonly List<AutomataState> _finalStates;

        public Dfa(AutomataState startState)
        {
            _startState = startState;
            _transitions = new Dictionary<AutomataState, Dictionary<char, AutomataState>>();
            _defaults = new Dictionary<AutomataState, AutomataState>();
            _finalStates = new List<AutomataState>();
        }

        public void AddFinalState(AutomataState states)
        {
            _finalStates.Add(states);
        }

        public void SetDefaultTransition(AutomataState src, AutomataState dest)
        {
            if (_defaults.ContainsKey(src))
                _defaults[src] = dest;
            else
                _defaults.Add(src, dest);
        }

        public void AddTransition(AutomataState src, char input, AutomataState dest)
        {
            Dictionary<char, AutomataState> set;
            if (_transitions.TryGetValue(src, out set))
            {
                if (set.ContainsKey(input))
                    set[input] = dest;
                else
                    set.Add(input, dest);
            }

            else
            {
                set = new Dictionary<char, AutomataState> {{input, dest}};
                _transitions.Add(src, set);
            }
        }

        public string WriteGraph()
        {
            string dotfile = "digraph dfa {\nnode [shape = circle];\n";

            foreach (var transition in _defaults)
            {
                string startState = transition.Key.State.Aggregate("", (current, entry) => current + (entry.Item1 + "_" + entry.Item2 + "_"));
                string destState = transition.Value.State.Aggregate("", (current, entry) => current + (entry.Item1 + "_" + entry.Item2 + "_"));
                startState = "N" + startState.Substring(0, startState.Length - 1);
                destState = "N" + destState.Substring(0, destState.Length - 1);
                dotfile += startState + " -> " + destState + ";\n";
            }

            foreach (var transition in _transitions)
            {
                string startState = transition.Key.State.Aggregate("", (current, entry) => current + (entry.Item1 + "_" + entry.Item2 + "_"));
                startState = "N" + startState.Substring(0, startState.Length - 1);

                foreach (KeyValuePair<char, AutomataState> state in transition.Value)
                {
                    string destState = transition.Value[state.Key].State.Aggregate("", (current, entry) => current + (entry.Item1 + "_" + entry.Item2 + "_"));
                    destState = "N" + destState.Substring(0, destState.Length - 1);
                    dotfile += startState + " -> " + destState + " [ label = \"" + state.Key + "\" ];\n";
                }
            }

            dotfile += "}";
            return dotfile;
        }

        public string FindNextValidString(string input)
        {
            AutomataState state = _startState;
            Stack<Tuple<string, AutomataState, char>> stack = new Stack<Tuple<string, AutomataState, char>>();
            int i = 0;

            foreach (char c in input)
            {
                stack.Push(Tuple.Create(input.Substring(0, i), state, c));
                state = FindNextState(state, c);

                if (state == null)
                    break;

                stack.Push(Tuple.Create(input.Substring(0, i+1), state, '\0'));
                i++;
            }

            if (IsFinal(state))
                return input;

            while (stack.Count > 0)
            {
                Tuple<string, AutomataState, char> item = stack.Pop();
                string path = item.Item1.TrimStart(System.IO.Path.GetInvalidFileNameChars());
                AutomataState itemState = item.Item2;
                char x = item.Item3;

                x = FindNextEdge(itemState, x);
                if (x != '\0')
                {
                    path += x;
                    itemState = FindNextState(itemState, x);
                    if (IsFinal(itemState))
                        return path;
                    stack.Push(Tuple.Create(path, itemState, '\u0001'));
                }
            }

            return null;
        }

        private AutomataState FindNextState(AutomataState src, char input)
        {
            if(_transitions.ContainsKey(src))
            {
                Dictionary<char, AutomataState> set = _transitions[src];
                AutomataState innerSet;
                if (set.TryGetValue(input, out innerSet))
                    return innerSet;
                if (_defaults.TryGetValue(src, out innerSet))
                    return innerSet;
                return null;
            }

            return null;
        }

        private bool IsFinal(AutomataState state)
        {
            return _finalStates.Contains(state);
        }

        public char FindNextEdge(AutomataState s, char x)
        {
            Dictionary<char, AutomataState> set;
            AutomataState automataState;
            if (x == '\0')
                x = '\u0001';
            x = (char)(x + 1);

            if (s == null || !_transitions.TryGetValue(s, out set))
                set = new Dictionary<char, AutomataState>();
            if (set.TryGetValue(x, out automataState) || (s != null && _defaults.TryGetValue(s, out automataState)))
                return x;

            List<char> orderedSet = set.Keys.OrderBy(y => y).ToList();

            int j = 0;
            foreach (var item in orderedSet)
            {
                if (x >= item)
                {
                    orderedSet.Insert(j, x);
                    break;
                }

                j++;
            }

            if (j >= orderedSet.Count && orderedSet.Count > 0)
                return orderedSet[orderedSet.Count - 1];
            if (j < orderedSet.Count)
                return orderedSet[j];
            return '\0';
        }
    }
}
