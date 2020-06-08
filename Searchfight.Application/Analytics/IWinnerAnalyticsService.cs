using System.Collections.Generic;
using Searchfight.Core.Domain;
using Searchfight.Core.ViewModels;

namespace Searchfight.Application.Analytics
{
    public interface IWinnerAnalyticsService
    {
        string GetWinnerSearchTerm(List<SearchResults> searchResults);

        List<SearchEngineWinner> GetSearchEngineWinners(List<SearchResults> searchResults);
    }
}