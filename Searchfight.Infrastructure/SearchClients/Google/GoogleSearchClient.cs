using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Searchfight.Core.Domain;
using Searchfight.Infrastructure.Configuration;

namespace Searchfight.Infrastructure.SearchClients.Google
{
    internal class GoogleSearchClient : BaseSearchClient, IGoogleSearchClient
    {
        public GoogleSearchClient(HttpClient httpClient, IInfrastructureConfiguration config):base(httpClient, config, SearchEngine.Google)
        {
            _baseUri = new StringBuilder($"https://www.googleapis.com/customsearch/v1?key={config.GoogleAPIKey}&cx={config.GoogleSearchEngineId}");
        }

        protected override HttpRequestMessage ComposeSearchRequest(string searchTerm)
        {
            return new HttpRequestMessage(HttpMethod.Get, _baseUri.Append($"&q={searchTerm}").ToString());
        }

        protected override async Task<ISearchResult> DeserializeJson(Stream stream, JsonSerializerOptions options)
        {
            return await JsonSerializer.DeserializeAsync<GoogleSearchResult>(stream, options);
        }
    }
}