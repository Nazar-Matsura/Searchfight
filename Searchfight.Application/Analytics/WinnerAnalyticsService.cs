using System.Collections.Generic;
using System.Linq;
using Searchfight.Core.Domain;
using Searchfight.Core.ViewModels;

namespace Searchfight.Application.Analytics
{
    internal class WinnerAnalyticsService : IWinnerAnalyticsService
    {
        public string GetWinnerSearchTerm(List<SearchResults> searchResults)
        {
            var maxResults = searchResults.Max(r => r.ResultsCount);
            return searchResults.First(r => r.ResultsCount == maxResults).SearchTerm;
        }

        public List<SearchEngineWinner> GetSearchEngineWinners(List<SearchResults> searchResults)
        {
            return searchResults
                .GroupBy(r => r.SearchEngine)
                .Select(g => new SearchEngineWinner(g.Key, GetWinnerSearchTerm(g.ToList())))
                .ToList();
        }
    }
}