using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Searchfight.Core.Domain;
using Searchfight.Infrastructure;

namespace Searchfight.Application
{
    internal class SearchfightService : ISearchfightService
    {
        protected readonly Dictionary<string, ISearchClient> _searchClients;

        public SearchfightService(IGoogleSearchClient googleSearchClient, IBingSearchClient bingSearchClient)
        {
            _searchClients = new Dictionary<string, ISearchClient>
            {
                { "Google", googleSearchClient },
                { "Bing", bingSearchClient }
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

        private async Task<SearchResults> CountSearchTermResult(string searchTerm, ISearchClient client, string clientName)
        {
            var count = await client.CountResults(searchTerm);
            return new SearchResults(clientName, searchTerm, count);
        }
    }
}