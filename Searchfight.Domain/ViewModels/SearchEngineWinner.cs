using Searchfight.Core.Domain;

namespace Searchfight.Core.ViewModels
{
    public class SearchEngineWinner
    {
        public SearchEngineWinner(SearchEngine searchEngine, string searchTerm)
        {
            SearchEngine = searchEngine;
            SearchTerm = searchTerm;
        }

        public SearchEngine SearchEngine { get; }

        public string SearchTerm { get; }
    }
}