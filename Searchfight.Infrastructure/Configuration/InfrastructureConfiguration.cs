using System.IO;
using Microsoft.Extensions.Configuration;

namespace Searchfight.Infrastructure.Configuration
{
    internal class InfrastructureConfiguration : IInfrastructureConfiguration
    {
        protected readonly IConfiguration _configuration;

        public InfrastructureConfiguration()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath($"{Directory.GetCurrentDirectory()}/Configuration")
                .AddJsonFile("infrastructure_settings.json", false)
                .Build();

            var test = _configuration.GetChildren();
        }

        public string GoogleAPIKey => _configuration["Google:APIKey"];

        public string GoogleSearchEngineId => _configuration["Google:SearchEngineID"];

        public string BingAPIKey => _configuration["Bing:APIKey"];
    }
}