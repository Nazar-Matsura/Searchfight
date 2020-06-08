using Microsoft.Extensions.DependencyInjection;

namespace Searchfight.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ISearchfightService, SearchfightService>();
            services.AddTransient<IMergeQuotedService, MergeQuotedService>();
        }
    }
}
