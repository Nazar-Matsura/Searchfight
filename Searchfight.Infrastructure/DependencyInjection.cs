using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Searchfight.Infrastructure.Configuration;
using Searchfight.Infrastructure.SearchClients.Bing;
using Searchfight.Infrastructure.SearchClients.Google;

namespace Searchfight.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IInfrastructureConfiguration, InfrastructureConfiguration>();
            services.AddTransient<IGoogleSearchClient, GoogleSearchClient>();
            services.AddTransient<IBingSearchClient, BingSearchClient>();
        }
    }
}
