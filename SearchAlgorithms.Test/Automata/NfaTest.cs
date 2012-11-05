using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SearchAlgorithms.Automata;

namespace SearchAlgorithms.Test.Automata
{
    public class NfaTest
    {
        private Nfa _nfa;
        [SetUp]
        public void Initialize()
        {
            _nfa = new Nfa();
        }

        [Test]
        public void NfaHasOneStartStateThatIsZeroBased()
        {
            Tuple<int, int> start = Tuple.Create(0, 0);

            Assert.AreEqual(1, _nfa.StartState.State.Count);
            Assert.AreEqual(start, _nfa.StartState.State.FirstOrDefault());
        }

        [Test]
        public void NfaHasValidAnyTransition()
        {
            Assert.AreEqual('Ä', Nfa.Any);
        }

        [Test]
        public void NfaHasValidEpsilonTransition()
        {
            Assert.AreEqual('Ë', Nfa.Epsilon);
        }

        [Test]
        public void NfaCanAddValidTransition()
        {
            const char input = 'e';
            Tuple<int, int> source = Tuple.Create(0, 0);
            Tuple<int, int> destination = Tuple.Create(1, 0);

            _nfa.AddTransition(source, input, destination);

            Dictionary<Tuple<int, int>, Dictionary<char, AutomataState>> expected =
                new Dictionary<Tuple<int, int>, Dictionary<char, AutomataState>>
                    {
                        {
                            source, new Dictionary<char, AutomataState>
                                {
                                    {input, new AutomataState(destination)}
                                }
                        }
                    };

            Assert.AreEqual(1, _nfa.Transitions.Count);
            Assert.AreEqual(1, _nfa.Transitions.FirstOrDefault().Value.Count);
            Assert.AreEqual(expected, _nfa.Transitions);
        }

        [Test]
        public void NfaCanAddSecondValidTransition()
        {
            const char input = 'e';
            Tuple<int, int> source = Tuple.Create(0, 0);
            Tuple<int, int> destination = Tuple.Create(1, 0);
            Tuple<int, int> destinationTwo = Tuple.Create(1, 0);

            _nfa.AddTransition(source, input, destination);
            _nfa.AddTransition(source, Nfa.Any, destinationTwo);

            Dictionary<Tuple<int, int>, Dictionary<char, AutomataState>> expected =
                new Dictionary<Tuple<int, int>, Dictionary<char, AutomataState>>
                    {
                        {
                            source, new Dictionary<char, AutomataState>
                                {
                                    {input, new AutomataState(destination)},
                                    {Nfa.Any, new AutomataState(destinationTwo)}
                                }
                        }
                    };

            Assert.AreEqual(1, _nfa.Transitions.Count);
            Assert.AreEqual(2, _nfa.Transitions.FirstOrDefault().Value.Count);
            Assert.AreEqual(expected, _nfa.Transitions);
        }

        [Test]
        public void NfaCanAddValidFinalState()
        {
            Tuple<int, int> final = Tuple.Create(1, 0);
            _nfa.AddFinalState(final);

            AutomataState expected =
                new AutomataState(final);

            Assert.AreEqual(expected, _nfa.FinalStates);
        }
    }
}
