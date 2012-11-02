using System.Collections.Generic;

namespace SearchAlgorithms
{
    public interface IBaseSearchAlgorithm<T>
    {
        IEnumerable<T> Search(T query, IEnumerable<T> dataset);
    }
}
