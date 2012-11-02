using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SearchAlgorithm.Surveyors.StringSurveyors;
using SearchAlgorithms.StringStrategies;

namespace SearchAlgorithms.Test.SearchStrategies
{
    public class LevenshteinAutomataStringStrategyTest
    {
        private List<string> _dataset;
        private LevenshteinAutomataStringStrategy _strategy;

        [SetUp]
        public void Initialize()
        {
            LevenshteinStringSurveyor surveyor = new LevenshteinStringSurveyor();
            _strategy = new LevenshteinAutomataStringStrategy(surveyor);
            _SetupDataset();
        }

        [Test]
        public void ShouldReturnStringsThatAreEqual()
        {
            IEnumerable<string> results = _strategy.Search("nice", _dataset, 0);
            Assert.AreEqual(1, results.Count());
        }

        private void _SetupDataset()
        {
            _dataset = new List<string>
                {
                    "nice",
                    "nici",
                    "rice",
                    "sonice",
                    "niceso",
                    "applepie",
                    "plepie",
                    "applep"
                };
        }
    }
}