using System.Collections.Generic;
using System.Threading.Tasks;
using Searchfight.Core.Domain;

namespace Searchfight.Application
{
    public interface ISearchfightService
    {
        Task<List<SearchResults>> CountSearchTermsResults(List<string> searchTerms);
    }
}