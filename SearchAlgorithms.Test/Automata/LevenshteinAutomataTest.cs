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
            _automata = new LevenshteinAutomata("nice", 1);
        }

        [Test]
        public void NfaIsCreatedWithNoError()
        {
            Nfa nfa = _automata.Construct();
            Assert.IsNotNull(nfa);
            string dotfile = nfa.WriteGraph();
            Assert.AreEqual(_nfaDotFile, dotfile);
        }

        [Test]
        public void DfaIsCreatedWithNoError()
        {
            Dfa dfa = _automata.Construct().ConstructDfaUsingPowerSet();
            Assert.IsNotNull(dfa);
            string dotfile = dfa.WriteGraph();
            Assert.AreEqual(_dfaDotFile, dotfile);
        }

        [Test]
        public void DfaCanFindNextValidState()
        {
            Dfa dfa = _automata.Construct().ConstructDfaUsingPowerSet();
            Assert.IsNotNull(dfa);

            string next = dfa.FindNextValidString("gice");
            Assert.AreEqual(next, "gice");
        }

        private string _nfaDotFile = "digraph nfa {\n" +
            "rankdir=LR;\n" +
            "node [shape = circle];\n" +
            "N0_0 -> N1_0 [ label = \"n\" ];\n" +
            "N0_0 -> N0_1 [ label = \"Ä\" ];\n" +
            "N0_0 -> N1_1 [ label = \"Ä\" ];\n" +
            "N0_0 -> N1_1 [ label = \"Ë\" ];\n" +
            "N0_1 -> N1_1 [ label = \"n\" ];\n" +
            "N1_0 -> N2_0 [ label = \"i\" ];\n" +
            "N1_0 -> N1_1 [ label = \"Ä\" ];\n" +
            "N1_0 -> N2_1 [ label = \"Ä\" ];\n" +
            "N1_0 -> N2_1 [ label = \"Ë\" ];\n" +
            "N1_1 -> N2_1 [ label = \"i\" ];\n" +
            "N2_0 -> N3_0 [ label = \"c\" ];\n" +
            "N2_0 -> N2_1 [ label = \"Ä\" ];\n" +
            "N2_0 -> N3_1 [ label = \"Ä\" ];\n" +
            "N2_0 -> N3_1 [ label = \"Ë\" ];\n" +
            "N2_1 -> N3_1 [ label = \"c\" ];\n" +
            "N3_0 -> N4_0 [ label = \"e\" ];\n" +
            "N3_0 -> N3_1 [ label = \"Ä\" ];\n" +
            "N3_0 -> N4_1 [ label = \"Ä\" ];\n" +
            "N3_0 -> N4_1 [ label = \"Ë\" ];\n" +
            "N3_1 -> N4_1 [ label = \"e\" ];\n" +
            "N4_0 -> N4_1 [ label = \"Ä\" ];\n" +
            "}";

            private string _dfaDotFile = "digraph dfa {\n" +
            "node [shape = circle];\n" +
            "N0_0_1_1 -> N0_1_1_1;\n" +
            "N1_0_0_1_1_1_2_1 -> N1_1_2_1;\n" +
            "N2_0_1_1_2_1_3_1 -> N2_1_3_1;\n" +
            "N3_0_2_1_3_1_4_1 -> N3_1_4_1;\n" +
            "N4_0_3_1_4_1 -> N4_1;\n" +
            "N0_0_1_1 -> N1_0_0_1_1_1_2_1 [ label = \"n\" ];\n" +
            "N0_0_1_1 -> N0_1_1_1_2_1 [ label = \"i\" ];\n" +
            "N0_1_1_1_2_1 -> N1_1 [ label = \"n\" ];\n" +
            "N0_1_1_1_2_1 -> N2_1 [ label = \"i\" ];\n" +
            "N0_1_1_1_2_1 -> N3_1 [ label = \"c\" ];\n" +
            "N3_1 -> N4_1 [ label = \"e\" ];\n" +
            "N2_1 -> N3_1 [ label = \"c\" ];\n" +
            "N1_1 -> N2_1 [ label = \"i\" ];\n" +
            "N0_1_1_1 -> N1_1 [ label = \"n\" ];\n" +
            "N0_1_1_1 -> N2_1 [ label = \"i\" ];\n" +
            "N1_0_0_1_1_1_2_1 -> N2_0_1_1_2_1_3_1 [ label = \"i\" ];\n" +
            "N1_0_0_1_1_1_2_1 -> N1_1_2_1_3_1 [ label = \"c\" ];\n" +
            "N1_1_2_1_3_1 -> N2_1 [ label = \"i\" ];\n" +
            "N1_1_2_1_3_1 -> N3_1 [ label = \"c\" ];\n" +
            "N1_1_2_1_3_1 -> N4_1 [ label = \"e\" ];\n" +
            "N1_1_2_1 -> N2_1 [ label = \"i\" ];\n" +
            "N1_1_2_1 -> N3_1 [ label = \"c\" ];\n" +
            "N2_0_1_1_2_1_3_1 -> N3_0_2_1_3_1_4_1 [ label = \"c\" ];\n" +
            "N2_0_1_1_2_1_3_1 -> N2_1_3_1_4_1 [ label = \"e\" ];\n" +
            "N2_1_3_1_4_1 -> N3_1 [ label = \"c\" ];\n" +
            "N2_1_3_1_4_1 -> N4_1 [ label = \"e\" ];\n" +
            "N2_1_3_1 -> N3_1 [ label = \"c\" ];\n" +
            "N2_1_3_1 -> N4_1 [ label = \"e\" ];\n" +
            "N3_0_2_1_3_1_4_1 -> N4_0_3_1_4_1 [ label = \"e\" ];\n" +
            "N3_1_4_1 -> N4_1 [ label = \"e\" ];\n" +
            "}";
    }
}
