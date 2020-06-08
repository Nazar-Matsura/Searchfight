namespace Searchfight.Core.Domain
{
    public class SearchResults
    {
        public SearchResults(SearchEngine searchEngine, string searchTerm, long resultsCount)
        {
            SearchEngine = searchEngine;
            SearchTerm = searchTerm;
            ResultsCount = resultsCount;
        }

        public SearchEngine SearchEngine { get; }

        public string SearchTerm { get; }

        public long ResultsCount { get; }
    }
}