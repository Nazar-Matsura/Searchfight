using System.Collections.Generic;
using Searchfight.Core.Domain;
using Searchfight.Core.ViewModels;

namespace Searchfight.Application
{
    public interface ISearchfightService
    {
        GeneralSearchfightViewModel GenerateReport(List<SearchResults> searchTermResults);
    }
}