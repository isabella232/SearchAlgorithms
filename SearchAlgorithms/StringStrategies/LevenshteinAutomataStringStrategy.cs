using System.Collections.Generic;
using SearchAlgorithm.Surveyors;

namespace SearchAlgorithms.StringStrategies
{
    public class LevenshteinAutomataStringStrategy : BaseSearchStrategy<string>
    {
        private readonly ISurveyor<string> _surveyor;

        public LevenshteinAutomataStringStrategy(ISurveyor<string> surveyor)
        {
            _surveyor = surveyor;
        }

        public override IEnumerable<string> Search(string query, IEnumerable<string> dataset, int fuzziness)
        {
            return null;
        }
    }
}