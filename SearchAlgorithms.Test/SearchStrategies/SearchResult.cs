namespace SearchAlgorithms.Test.SearchStrategies
{
    public class SearchResult
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Number { get; set; }

        public SearchResult(string id, string description, string number)
        {
            Id = id;
            Description = description;
            Number = number;
        }
    }
}
