using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Searchfight.Infrastructure.Configuration;

namespace Searchfight.Infrastructure
{
    internal class GoogleSearchClient : IGoogleSearchClient
    {
        private readonly HttpClient _httpClient;
        private readonly StringBuilder _baseUri;

        public GoogleSearchClient(HttpClient httpClient, IInfrastructureConfiguration config)
        {
            _httpClient = httpClient;
            _baseUri = new StringBuilder($"https://www.googleapis.com/customsearch/v1?key={config.GoogleAPIKey}&cx={config.GoogleSearchEngineId}");
        }

        public async Task<long> CountResults(string searchTerm)
        {
            var query = BuildCountResultsQuery(searchTerm);
            var response = await _httpClient.GetStreamAsync(query);
            return await ExtractTotalResultsCount(response);
        }

        private async Task<long> ExtractTotalResultsCount(Stream responseStream)
        {
            var serializerOptions = new JsonSerializerOptions {PropertyNameCaseInsensitive = true};
            var response = await JsonSerializer.DeserializeAsync<GoogleSearchResult>(responseStream, serializerOptions);
            return long.Parse(response.SearchInformation.TotalResults);
        }

        private Uri BuildCountResultsQuery(string searchTerm)
        {
            var uriString = _baseUri.Append($"&q={searchTerm}").ToString();
            return new Uri(uriString);
        }
    }
}