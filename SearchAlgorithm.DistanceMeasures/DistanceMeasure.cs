using System;

namespace SearchAlgorithm.DistanceMeasures
{
    public class DistanceMeasure<T> : IDistanceMeasure<T>
    {
        public virtual int CalculateDistance(T root, T leaf)
        {
            // abstract generic implemention does not make sense
            // you must override
            throw new NotImplementedException();
        }
    }
}