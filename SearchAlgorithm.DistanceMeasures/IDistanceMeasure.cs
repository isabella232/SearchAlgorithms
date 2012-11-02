namespace SearchAlgorithm.DistanceMeasures
{
    public interface IDistanceMeasure<T>
    {
        int CalculateDistance(T root, T leaf);
    }
}
