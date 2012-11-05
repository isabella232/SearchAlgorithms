using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SearchAlgorithm.Surveyors;
using SearchAlgorithms.Automata;

namespace SearchAlgorithms.StringStrategies
{
    /// <summary>
    /// Fuzzy search thats really efficent
    /// Implemented based on http://blog.notdot.net/2010/07/Damn-Cool-Algorithms-Levenshtein-Automata
    /// </summary>
    public class LevenshteinAutomataStringStrategy : BaseSearchStrategy<string>
    {
        private readonly ISurveyor<string> _surveyor;

        public LevenshteinAutomataStringStrategy(ISurveyor<string> surveyor)
        {
            _surveyor = surveyor;
        }

        /// <summary>
        /// Search a given dataset for the given query 
        /// </summary>
        /// <param name="query">What you are searching for</param>
        /// <param name="dataset">The dataset to search</param>
        /// <param optional="true" name="fuzziness">
        /// The distance away from root to search. Default is 0 which means only exact matches can be found
        /// </param>
        /// <returns>A list of items found in the dataset given the query</returns>
        public override IEnumerable<string> Search(string query, IList<string> dataset, int fuzziness = 0)
        {
            List<string> matches = new List<string>();
            // construct levenshtein automata for query (Aw)
            Dfa queryAutomata = _ConstructLevenshteinDfa(query, fuzziness);
            string current = "\u0001";
            string next = "";
            int index = 0;

            while (next != null)
            {
                next = queryAutomata.FindNextValidString(current);

                if (next == current && dataset.Contains(next))
                {
                    matches.Add(next);
                    index = dataset.IndexOf(next);
                    current = dataset[index + 1];
                }

                else
                    current = next;
            }

            return matches;
        }

        private Dfa _ConstructLevenshteinDfa(string query, int fuzziness)
        {
            LevenshteinAutomata automata = new LevenshteinAutomata(query, fuzziness);
            return automata.Construct().ConstructDfaUsingPowerSet();
        }
    }
}