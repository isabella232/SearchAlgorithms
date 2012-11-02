using NUnit.Framework;
using SearchAlgorithm.DistanceMeasures;

namespace SearchAlgorithms.Test.DistanceMeasures
{
    public class LevenshteinDistanceBetweenStringsTest
    {
        private LevenshteinDistanceBetweenStrings _distance;
        [SetUp]
        public void Initialize()
        {
            _distance = new LevenshteinDistanceBetweenStrings();
        }

        [Test]
        public void EqualStringsShouldComputeZero()
        {
            string root = "nice";
            string leaf = "nice";

            int distance = _distance.CalculateDistance(root, leaf);

            Assert.AreEqual(0, distance);
        }

        [Test]
        public void StringsWithLastCharacterSubstitutedShouldComputeOne()
        {
            string root = "nice";
            string leaf = "nici";

            int distance = _distance.CalculateDistance(root, leaf);

            Assert.AreEqual(1, distance);
        }

        [Test]
        public void StringsWithFirstCharacterSubstituedShouldComputeOne()
        {
            string root = "nice";
            string leaf = "rice";

            int distance = _distance.CalculateDistance(root, leaf);

            Assert.AreEqual(1, distance);
        }

        [Test]
        public void StringsWithTwoCharacterAddedToFrontShouldComputeTwo()
        {
            string root = "nice";
            string leaf = "sonice";

            int distance = _distance.CalculateDistance(root, leaf);

            Assert.AreEqual(2, distance);
        }

        [Test]
        public void StringsWithTwoCharacterAddedToEndShouldComputeTwo()
        {
            string root = "nice";
            string leaf = "niceso";

            int distance = _distance.CalculateDistance(root, leaf);

            Assert.AreEqual(2, distance);
        }

        [Test]
        public void StringsWithTwoCharacterRemovedFromFrontShouldComputeTwo()
        {
            string root = "applepie";
            string leaf = "plepie";

            int distance = _distance.CalculateDistance(root, leaf);

            Assert.AreEqual(2, distance);
        }

        [Test]
        public void StringsWithTwoCharacterRemovedFromEndShouldComputeTwo()
        {
            string root = "applepie";
            string leaf = "applep";

            int distance = _distance.CalculateDistance(root, leaf);

            Assert.AreEqual(2, distance);
        }
    }
}
