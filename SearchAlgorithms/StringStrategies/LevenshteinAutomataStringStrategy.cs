using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SearchAlgorithms.Automata;

namespace SearchAlgorithms.StringStrategies
{
    /// <summary>
    /// Fuzzy search thats really efficent
    /// Implemented based on http://blog.notdot.net/2010/07/Damn-Cool-Algorithms-Levenshtein-Automata
    /// </summary>
    public class LevenshteinAutomataStringStrategy : BaseSearchStrategy<string>
    {
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

            Dfa levenshteinAutomata = _LevenshteinAutomata(query, fuzziness);
            string match = levenshteinAutomata.FindNextValidString("\u0001");

            while (match != null)
            {
                string next = LookupFunc(match, dataset);
                if (next == null)
                    break;

                int index = dataset.IndexOf(next);
                dataset.RemoveAt(index);
                if (next.Contains(match))
                    matches.Add(next);

                next = dataset[index];
                match = levenshteinAutomata.FindNextValidString(next);
            }

            return matches;
        }

        private string LookupFunc(string match, IList<string> dataset)
        {
            int imax = dataset.Count - 1;
            int imin = 0;

            while (imax >= imin)
            {
                int imid = imin + ((imax - imin) / 2);

                if (string.CompareOrdinal(dataset[imid], match) < 0)
                    imin = imid + 1;
                else if (string.CompareOrdinal(dataset[imid],match) > 0)
                    imax = imid - 1;
                else
                    return dataset[imid];
            }

            if (imin >= dataset.Count)
                return null;

            return dataset[imin];
        }

        private Dfa _LevenshteinAutomata(string query, int fuzziness)
        {
            LevenshteinAutomata automata = new LevenshteinAutomata(query, fuzziness);
            return automata.Construct().ConstructDfaUsingPowerSet();
        }
    }
}