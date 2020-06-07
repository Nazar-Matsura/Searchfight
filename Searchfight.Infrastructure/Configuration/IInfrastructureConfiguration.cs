namespace Searchfight.Infrastructure.Configuration
{
    interface IInfrastructureConfiguration
    {
        string GoogleAPIKey { get; }

        string GoogleSearchEngineId { get; }

        string BingAPIKey { get; }
    }
}