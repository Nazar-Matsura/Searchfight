using System.Collections.Generic;
using System.Linq;
using Searchfight.Application.Analytics;
using Searchfight.Core.Domain;
using Searchfight.Core.ViewModels;

namespace Searchfight.Application
{
    internal class SearchfightService : ISearchfightService
    {
        protected readonly IWinnerAnalyticsService _winnerAnalyticsService;

        protected readonly IBySearchTermAnalyticsService _bySearchTermAnalyticsService;

        public SearchfightService(IWinnerAnalyticsService winnerAnalyticsService, 
            IBySearchTermAnalyticsService bySearchTermAnalyticsService)
        {
            _winnerAnalyticsService = winnerAnalyticsService;
            _bySearchTermAnalyticsService = bySearchTermAnalyticsService;
        }

        public GeneralSearchfightViewModel GenerateReport(List<SearchResults> searchResults)
        {
            var resultsBySearchTerm = _bySearchTermAnalyticsService.GetReport(searchResults);

            var resultsBySearchEngine = _winnerAnalyticsService.GetSearchEngineWinners(searchResults);

            var winner = _winnerAnalyticsService.GetWinnerSearchTerm(searchResults);

            return new GeneralSearchfightViewModel(resultsBySearchTerm, resultsBySearchEngine, winner);
        }
    }
}