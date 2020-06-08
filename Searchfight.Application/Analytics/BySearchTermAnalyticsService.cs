using System.Collections.Generic;
using System.Linq;
using Searchfight.Core.Domain;
using Searchfight.Core.ViewModels;

namespace Searchfight.Application.Analytics
{
    internal class BySearchTermAnalyticsService : IBySearchTermAnalyticsService
    {
        public List<SearchTermResults> GetReport(List<SearchResults> searchResults)
        {
            return searchResults
                .GroupBy(r => r.SearchTerm)
                .Select(g => new SearchTermResults(g.Key,
                    g.OrderBy(r => r.SearchEngine)
                        .Select(r => new SearchEngineResultsCount(r.SearchEngine, r.ResultsCount))
                        .ToList()))
                .ToList();
        }
    }
}