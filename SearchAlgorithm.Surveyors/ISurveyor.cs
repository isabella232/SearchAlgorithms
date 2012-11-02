namespace SearchAlgorithm.Surveyors
{
    public interface ISurveyor<T>
    {
        int CalculateDistance(T root, T leaf);
    }
}
