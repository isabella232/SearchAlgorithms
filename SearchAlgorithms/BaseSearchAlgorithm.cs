using System.Collections.Generic;

namespace SearchAlgorithms
{
    public class BaseSearchAlgorithm<T> : IBaseSearchAlgorithm<T>
    {
        public virtual IEnumerable<T> Search(T query, IEnumerable<T> dataset)
        {
            // abstract generic implemention does not make sense
            // you must override
            throw new System.NotImplementedException();
        }
    }
}