using System.Collections.Generic;
using System.Linq;
using Searchfight.Core.Domain;
using Searchfight.Core.ViewModels;

namespace Searchfight.Application
{
    internal class SearchfightService : ISearchfightService
    {
        public GeneralSearchfightViewModel GenerateReport(List<SearchResults> searchTermResults)
        {
            var resultsBySearchTerm = BuildResultsBySearchTerm(searchTermResults);

            var resultsBySearchEngine = BuildResultsBySearchEngine(searchTermResults);

            var winner = GetWinner(searchTermResults);

            return new GeneralSearchfightViewModel(resultsBySearchTerm, resultsBySearchEngine, winner);
        }

        private string GetWinner(List<SearchResults> searchTermResults)
        {
            var maxResults = searchTermResults.Max(r => r.ResultsCount);
            return searchTermResults.First(r => r.ResultsCount == maxResults).SearchTerm;
        }
        
        private List<SearchEngineWinner> BuildResultsBySearchEngine(List<SearchResults> searchTermResults)
        {
            return searchTermResults
                .GroupBy(r => r.SearchEngine)
                .Select(g => new SearchEngineWinner(g.Key, GetWinner(g.ToList())))
                .ToList();
        }

        private List<SearchTermResults> BuildResultsBySearchTerm(List<SearchResults> searchTermResults)
        {
            return searchTermResults
                .GroupBy(r => r.SearchTerm)
                .Select(g => new SearchTermResults(g.Key,
                    g.Select(r => new SearchEngineResultsCount(r.SearchEngine, r.ResultsCount)).ToList()))
                .ToList();
        }
    }
}