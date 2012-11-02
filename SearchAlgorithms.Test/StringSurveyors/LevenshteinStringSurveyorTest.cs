using NUnit.Framework;
using SearchAlgorithm.DistanceMeasures;

namespace SearchAlgorithms.Test.DistanceMeasures
{
    public class LevenshteinStringSurveyorTest
    {
        private LevenshteinStringSurveyor _surveyor;
        [SetUp]
        public void Initialize()
        {
            _surveyor = new LevenshteinStringSurveyor();
        }

        [Test]
        public void EqualStringsShouldComputeZero()
        {
            string root = "nice";
            string leaf = "nice";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(0, distance);
        }

        [Test]
        public void StringsWithLastCharacterSubstitutedShouldComputeOne()
        {
            string root = "nice";
            string leaf = "nici";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(1, distance);
        }

        [Test]
        public void StringsWithFirstCharacterSubstituedShouldComputeOne()
        {
            string root = "nice";
            string leaf = "rice";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(1, distance);
        }

        [Test]
        public void StringsWithTwoCharacterAddedToFrontShouldComputeTwo()
        {
            string root = "nice";
            string leaf = "sonice";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(2, distance);
        }

        [Test]
        public void StringsWithTwoCharacterAddedToEndShouldComputeTwo()
        {
            string root = "nice";
            string leaf = "niceso";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(2, distance);
        }

        [Test]
        public void StringsWithTwoCharacterRemovedFromFrontShouldComputeTwo()
        {
            string root = "applepie";
            string leaf = "plepie";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(2, distance);
        }

        [Test]
        public void StringsWithTwoCharacterRemovedFromEndShouldComputeTwo()
        {
            string root = "applepie";
            string leaf = "applep";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(2, distance);
        }
    }
}
