using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SearchAlgorithms.Automata;

namespace SearchAlgorithms.Test.Automata
{
    public class LevenshteinAutomataTest
    {
        private LevenshteinAutomata _automata;

        [SetUp]
        public void Initialize()
        {
            _automata = new LevenshteinAutomata("food", 1);
        }

        [Test]
        public void NfaIsCreatedWithNoError()
        {
            Nfa nfa = _automata.Construct();
            Assert.IsNotNull(nfa);
        }

        [Test]
        public void DfaIsCreatedWithNoError()
        {
            Dfa dfa = _automata.Construct().ConstructDfaUsingPowerSet();
            Assert.IsNotNull(dfa);
        }

        [Test]
        public void DfaCanFindNextValidState()
        {
            Dfa dfa = _automata.Construct().ConstructDfaUsingPowerSet();
            Assert.IsNotNull(dfa);

            string next = dfa.FindNextValidString("foo");
            Assert.AreEqual(next, "foo");
        }
    }
}
