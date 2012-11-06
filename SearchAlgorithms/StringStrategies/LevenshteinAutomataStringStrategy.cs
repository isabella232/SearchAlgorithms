using System.Collections;
using System.Collections.Generic;
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
            SortedList sortedDataSet = new SortedList(dataset.ToDictionary(str => str));

            Dfa levenshteinAutomata = _LevenshteinAutomata(query, fuzziness);
            string match = levenshteinAutomata.FindNextValidString("\u0001");

            while (match != null)
            {
                string next = LookupFunc(match, sortedDataSet);
                if (next == null)
                    break;

                if (next == match)
                {
                    matches.Add(next);
                    int index = sortedDataSet.IndexOfKey(next);
                    sortedDataSet.RemoveAt(index);
                    next = sortedDataSet.GetByIndex(index) as string;
                }

                match = levenshteinAutomata.FindNextValidString(next);
            }

            return matches;
        }

        private string LookupFunc(string match, SortedList dataset)
        {
            int imax = dataset.Count - 1;
            int imin = 0;

            // continue searching while [imin,imax] is not empty
            while (imax >= imin)
            {
                /* calculate the midpoint for roughly equal partition */
                int imid = imin + ((imax - imin) / 2);

                // determine which subarray to search
                if (string.CompareOrdinal(dataset.GetByIndex(imid) as string, match) < 0)
                    // change min index to search upper subarray
                    imin = imid + 1;
                else if (string.CompareOrdinal(dataset.GetByIndex(imid) as string,match) > 0)
                    // change max index to search lower subarray
                    imax = imid - 1;
                else
                    // key found at index imid
                    return dataset.GetByIndex(imid) as string;
            }

            //if (_justUsed == dataset.GetByIndex(imin) as string)
                //imin++;

            if (imin >= dataset.Count)
                return null;

            //_justUsed = dataset.GetByIndex(imin) as string;
            return dataset.GetByIndex(imin) as string;
        }

        private Dfa _LevenshteinAutomata(string query, int fuzziness)
        {
            LevenshteinAutomata automata = new LevenshteinAutomata(query, fuzziness);
            return automata.Construct().ConstructDfaUsingPowerSet();
        }
    }
}