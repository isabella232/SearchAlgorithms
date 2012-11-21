using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;
using SearchAlgorithms.StringStrategies;

namespace SearchAlgorithms.Test.SearchStrategies
{
    public class LevenshteinAutomataStringStrategyTest
    {
        private IList<string> _dataset;
        private List<string> _largeDataset;
        private LevenshteinAutomataStringStrategy _strategy;
        private SortedList _sortedDataSet;

        [SetUp]
        public void Initialize()
        {
            _strategy = new LevenshteinAutomataStringStrategy();
            _SetupDataset();
            _SetupLargeDataset();
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

        [Test]
        public void ShouldReturnStringsFromLargeDatasetThatAreEqual()
        {
            IEnumerable<string> results = _strategy.Search("Will Barken", _largeDataset);
            Assert.AreEqual(1, results.Count());
        }

        [Test]
        public void ShouldReturnStringsFromLargeDatasetThatAreNotEqual()
        {
            string[] dataset = new string[_sortedDataSet.Count];
            _sortedDataSet.Values.CopyTo(dataset, 0);
            IEnumerable<string> results = _strategy.Search("Randy".ToLower(), dataset.ToList(), 1);

            List<string> finalResults = new List<string>();
            foreach (string item in _largeDataset)
            {
                foreach (string result in results)
                    if (item.ToLower().Contains(result))
                        finalResults.Add(item);
            }

            Assert.AreEqual(377, finalResults.Count);
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

        private void _SetupLargeDataset()
        {
            _largeDataset = new List<string>();
            HashSet<SearchResult> searchResults = new HashSet<SearchResult>();
            StreamReader reader = new StreamReader(@"results.csv");
            reader.ReadLine(); //do not process header

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] entries = line.Split(new [] {','}, System.StringSplitOptions.None);
                SearchResult sr = new SearchResult(entries[0], entries[1], entries[2]);
                searchResults.Add(sr);
            }

            _largeDataset.AddRange(searchResults.Where(x => !string.IsNullOrEmpty(x.Description)).Select(x => x.Description));
            _largeDataset.AddRange(searchResults.Where(x => !string.IsNullOrEmpty(x.Number)).Select(x => x.Number));
            List<string> intermediateDataSet = new List<string>();
            intermediateDataSet.AddRange(_largeDataset.SelectMany(x => x.Split(new[] {' '})));

            _sortedDataSet = new SortedList(
                intermediateDataSet.Where(x => !string.IsNullOrWhiteSpace(x)).
                                    Select(x => x.ToLower().Trim()).
                                    Distinct().
                                    ToDictionary(str => str));
        }
    }
}