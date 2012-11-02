using System;

namespace SearchAlgorithm.Surveyors
{
    public class Surveyor<T> : ISurveyor<T>
    {
        public virtual int CalculateDistance(T root, T leaf)
        {
            // abstract generic implemention does not make sense
            // you must override
            throw new NotImplementedException();
        }
    }
}