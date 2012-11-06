using System;
using System.Collections.Generic;
using System.Linq;

namespace SearchAlgorithms.Automata
{
    public class AutomataState : IEquatable<AutomataState>
    {
        public bool Equals(AutomataState other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return State.SequenceEqual(other.State);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AutomataState) obj);
        }

        public override int GetHashCode()
        {
            return State.Aggregate(0, (current, entry) => current.GetHashCode() + entry.GetHashCode());
        }

        public static bool operator ==(AutomataState left, AutomataState right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(AutomataState left, AutomataState right)
        {
            return !Equals(left, right);
        }

        public HashSet<Tuple<int, int>> State { get; set; }

        public AutomataState(HashSet<Tuple<int, int>> newState)
        {
            State = newState;
        }

        public AutomataState(Tuple<int, int> newState)
        {
            State = new HashSet<Tuple<int, int>> {newState};
        }

        public AutomataState()
        {
            State = new HashSet<Tuple<int, int>>();
        }

        public void Add(Tuple<int, int> dest)
        {
            if (State != null)
                State.Add(dest);
            else
                State = new HashSet<Tuple<int, int>> {dest};
        }
    }
}
