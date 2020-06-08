using System.Collections.Generic;

namespace Searchfight.Core.ViewModels
{
    public class SearchTermResults
    {
        public SearchTermResults(string searchTerm, List<SearchEngineResultsCount> searchEngineResults)
        {
            SearchTerm = searchTerm;
            SearchEngineResults = searchEngineResults;
        }

        public string SearchTerm { get; }

        public List<SearchEngineResultsCount> SearchEngineResults { get; }
    }
}