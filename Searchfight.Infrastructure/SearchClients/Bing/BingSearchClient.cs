using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Searchfight.Infrastructure.Configuration;

namespace Searchfight.Infrastructure.SearchClients.Bing
{
    class BingSearchClient : IBingSearchClient
    {
        private readonly HttpClient _httpClient;
        private readonly StringBuilder _baseUri;
        private readonly IInfrastructureConfiguration _config;

        public BingSearchClient(HttpClient httpClient, IInfrastructureConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _baseUri = new StringBuilder("https://api.cognitive.microsoft.com/bing/v7.0/search");
        }
        
        public async Task<long> CountResults(string searchTerm)
        {
            var request = ComposeSearchRequest(searchTerm);
            var response = await _httpClient.SendAsync(request);
            var responseStream = await response.Content.ReadAsStreamAsync();
            return await ExtractCountResultsFromResponse(responseStream);
        }

        private async Task<long> ExtractCountResultsFromResponse(Stream responseStream)
        {
            var options = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
            var response = await JsonSerializer.DeserializeAsync<BingSearchResult>(responseStream, options);
            var estimatedMatches = response.WebPages.TotalEstimatedMatches;
            return estimatedMatches;
        }

        private HttpRequestMessage ComposeSearchRequest(string searchTerm)
        {
            var url = new Uri(_baseUri.Append($"?q={searchTerm}").ToString());
            var result = new HttpRequestMessage(HttpMethod.Get, url);
            result.Headers.Add("Ocp-Apim-Subscription-Key", _config.BingAPIKey);

            return result;
        }
    }
}