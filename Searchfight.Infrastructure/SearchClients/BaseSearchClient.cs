using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Searchfight.Core.Domain;
using Searchfight.Infrastructure.Configuration;
using Searchfight.Infrastructure.SearchClients.Exceptions;

namespace Searchfight.Infrastructure.SearchClients
{
    internal abstract class BaseSearchClient : ISearchClient
    {
        public SearchEngine SearchEngine { get; }

        protected readonly HttpClient _httpClient;
        protected readonly IInfrastructureConfiguration _config;
        protected StringBuilder _baseUri;

        protected BaseSearchClient(HttpClient httpClient,
            IInfrastructureConfiguration config,
            SearchEngine searchEngine)
        {
            _httpClient = httpClient;
            _config = config;
            SearchEngine = searchEngine;
        }

        public virtual async Task<long> CountResults(string searchTerm)
        {
            var request = ComposeSearchRequest(searchTerm);

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new SearchClientException(SearchEngine, ex);
            }

            if (!response.IsSuccessStatusCode)
                throw new SearchClientException(SearchEngine, httpFailedResponse: response);

            return await ExtractCountResultsFromResponse(response);
        }

        protected abstract HttpRequestMessage ComposeSearchRequest(string searchTerm);

        protected virtual async Task<long> ExtractCountResultsFromResponse(HttpResponseMessage response)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
            var responseObject = await DeserializeJson(stream, options);
            var estimatedMatches = responseObject.TotalResults;
            return estimatedMatches;
        }

        protected abstract Task<ISearchResult> DeserializeJson(Stream stream, JsonSerializerOptions options);
    }
}