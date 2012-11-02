using System.Collections.Generic;

namespace SearchAlgorithms
{
    public class BaseSearchStrategy<T> : IBaseSearchStrategy<T>
    {
        public virtual IEnumerable<T> Search(T query, IEnumerable<T> dataset, int fuzziness)
        {
            // abstract generic implemention does not make sense
            // you must override
            throw new System.NotImplementedException();
        }
    }
}