using System.Collections.Generic;
using System.Threading.Tasks;
using Searchfight.Core.Domain;

namespace Searchfight.Application
{
    public interface ISearchDataProvider
    {
        Task<List<SearchResults>> CountSearchTermsResults(List<string> searchTerms);
    }
}