namespace SearchAlgorithm.DistanceMeasures
{
    public interface ISurveyor<T>
    {
        int CalculateDistance(T root, T leaf);
    }
}
