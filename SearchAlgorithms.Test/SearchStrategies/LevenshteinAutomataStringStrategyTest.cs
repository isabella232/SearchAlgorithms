using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SearchAlgorithm.Surveyors.StringSurveyors;
using SearchAlgorithms.StringStrategies;

namespace SearchAlgorithms.Test.SearchStrategies
{
    public class LevenshteinAutomataStringStrategyTest
    {
        private IList<string> _dataset;
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
            IEnumerable<string> results = _strategy.Search("nice", _dataset);
            Assert.AreEqual(1, results.Count());
        }

        [Test]
        public void ShouldReturnStringsThatAreOnlyOffByAtMostOneCharacter()
        {
            IEnumerable<string> results = _strategy.Search("nice", _dataset, 1);
            Assert.AreEqual(3, results.Count());
        }

        [Test]
        public void ShouldReturnStringsThatAreOnlyOffByAtMostTwoCharacters()
        {
            IEnumerable<string> results = _strategy.Search("nice", _dataset, 2);
            Assert.AreEqual(5, results.Count());
        }

        private void _SetupDataset()
        {
            _dataset = new List<string>
                {
                    "applepie",
                    "nice",
                    "rice",
                    "niceso",
                    "plepie",
                    "applep",
                    "genes",
                    "beer",
                    "nici",
                    "wizened",
                    "great",
                    "bait",
                    "sonice",
                    "mate"
                };
        }
    }
}