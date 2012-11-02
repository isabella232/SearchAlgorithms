using NUnit.Framework;
using SearchAlgorithm.DistanceMeasures.StringSurveyors;

namespace SearchAlgorithms.Test.StringSurveyors
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
            const string root = "nice";
            const string leaf = "nice";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(0, distance);
        }

        [Test]
        public void StringsWithLastCharacterSubstitutedShouldComputeOne()
        {
            const string root = "nice";
            const string leaf = "nici";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(1, distance);
        }

        [Test]
        public void StringsWithFirstCharacterSubstituedShouldComputeOne()
        {
            const string root = "nice";
            const string leaf = "rice";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(1, distance);
        }

        [Test]
        public void StringsWithTwoCharacterAddedToFrontShouldComputeTwo()
        {
            const string root = "nice";
            const string leaf = "sonice";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(2, distance);
        }

        [Test]
        public void StringsWithTwoCharacterAddedToEndShouldComputeTwo()
        {
            const string root = "nice";
            const string leaf = "niceso";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(2, distance);
        }

        [Test]
        public void StringsWithTwoCharacterRemovedFromFrontShouldComputeTwo()
        {
            const string root = "applepie";
            const string leaf = "plepie";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(2, distance);
        }

        [Test]
        public void StringsWithTwoCharacterRemovedFromEndShouldComputeTwo()
        {
            const string root = "applepie";
            const string leaf = "applep";

            int distance = _surveyor.CalculateDistance(root, leaf);

            Assert.AreEqual(2, distance);
        }
    }
}
