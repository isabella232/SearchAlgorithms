using System.Collections.Generic;

namespace SearchAlgorithms
{
    public interface IBaseSearchStrategy<T>
    {
        IEnumerable<T> Search(T query, IEnumerable<T> dataset);
    }
}
