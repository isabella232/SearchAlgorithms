using System.Collections.Generic;

namespace SearchAlgorithms
{
    public interface IBaseSearchStrategy<T>
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
        IEnumerable<T> Search(T query, IList<T> dataset, int fuzziness = 0);
    }
}
