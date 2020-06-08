using Searchfight.Core.Domain;

namespace Searchfight.Core.ViewModels
{
    public class SearchEngineResultsCount
    {
        public SearchEngineResultsCount(SearchEngine searchEngine, long resultsCount)
        {
            SearchEngine = searchEngine;
            ResultsCount = resultsCount;
        }

        public SearchEngine SearchEngine { get; }
        public long ResultsCount { get; }
    }
}