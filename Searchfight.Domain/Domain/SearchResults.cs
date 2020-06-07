namespace Searchfight.Core.Domain
{
    public class SearchResults
    {
        public SearchResults(string searchEngineName, string searchTerm, long resultsCount)
        {
            SearchEngineName = searchEngineName;
            SearchTerm = searchTerm;
            ResultsCount = resultsCount;
        }

        public string SearchEngineName { get; }

        public string SearchTerm { get; }

        public long ResultsCount { get; }
    }
}