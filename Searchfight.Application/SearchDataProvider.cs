using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Searchfight.Core.Domain;
using Searchfight.Infrastructure.SearchClients;
using Searchfight.Infrastructure.SearchClients.Bing;
using Searchfight.Infrastructure.SearchClients.Google;

namespace Searchfight.Application
{
    internal class SearchDataProvider : ISearchDataProvider
    {
        protected readonly Dictionary<SearchEngine, ISearchClient> _searchClients;

        public SearchDataProvider(IGoogleSearchClient googleSearchClient, IBingSearchClient bingSearchClient)
        {
            _searchClients = new Dictionary<SearchEngine, ISearchClient>
            {
                { SearchEngine.Google, googleSearchClient },
                { SearchEngine.Bing, bingSearchClient }
            };
        }

        public async Task<List<SearchResults>> CountSearchTermsResults(List<string> searchTerms)
        {
            var searchTasks = new List<Task<SearchResults>>();
            foreach (var searchClient in _searchClients)
            {
                foreach (var searchTerm in searchTerms)
                {
                    searchTasks.Add(CountSearchTermResult(searchTerm, searchClient.Value, searchClient.Key));
                }
            }

            var results = await Task.WhenAll(searchTasks);

            return results.ToList();
        }

        private async Task<SearchResults> CountSearchTermResult(string searchTerm, ISearchClient client, SearchEngine searchEngine)
        {
            var count = await client.CountResults(searchTerm);
            return new SearchResults(searchEngine, searchTerm, count);
        }
    }
}