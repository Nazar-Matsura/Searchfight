using Microsoft.Extensions.DependencyInjection;
using Searchfight.Application.Analytics;

namespace Searchfight.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ISearchDataProvider, SearchDataProvider>();

            services.AddTransient<IWinnerAnalyticsService, WinnerAnalyticsService>();
            services.AddTransient<IBySearchTermAnalyticsService, BySearchTermAnalyticsService>();

            services.AddTransient<ISearchfightService, SearchfightService>();

            services.AddTransient<IMergeQuotedService, MergeQuotedService>();
        }
    }
}
