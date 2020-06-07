using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Searchfight.Application;
using Searchfight.Infrastructure;

namespace Searchfight.Terminal
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            var app = serviceProvider.GetService<App>();
            await app.Run(args);
        }
        
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddTransient<App>();
            services.AddInfrastructure();
            services.AddApplication();

            return services;
        }
    }
}
