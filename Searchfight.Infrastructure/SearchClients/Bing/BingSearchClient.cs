using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Searchfight.Core.Domain;
using Searchfight.Infrastructure.Configuration;

namespace Searchfight.Infrastructure.SearchClients.Bing
{
    class BingSearchClient : BaseSearchClient, IBingSearchClient
    {
        public BingSearchClient(HttpClient httpClient, IInfrastructureConfiguration config)
            : base(httpClient, config, SearchEngine.Bing)
        {
            _baseUri = new StringBuilder("https://api.cognitive.microsoft.com/bing/v7.0/search");
        }

        protected override async Task<ISearchResult> DeserializeJson(Stream stream, JsonSerializerOptions options)
        {
            return await JsonSerializer.DeserializeAsync<BingSearchResult>(stream, options);
        }

        protected override HttpRequestMessage ComposeSearchRequest(string searchTerm)
        {
            var url = new Uri(_baseUri.Append($"?q={searchTerm}").ToString());
            var result = new HttpRequestMessage(HttpMethod.Get, url);
            result.Headers.Add("Ocp-Apim-Subscription-Key", _config.BingAPIKey);

            return result;
        }
    }
}