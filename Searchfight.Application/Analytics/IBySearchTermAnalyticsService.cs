using System.Collections.Generic;
using Searchfight.Core.Domain;
using Searchfight.Core.ViewModels;

namespace Searchfight.Application.Analytics
{
    public interface IBySearchTermAnalyticsService
    {
        List<SearchTermResults> GetReport(List<SearchResults> searchTermResults);
    }
}