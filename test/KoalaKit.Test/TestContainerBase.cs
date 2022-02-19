using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Test
{
    public abstract class TestContainerBase
    {
        protected TestContainerBase()
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings-test.json")
                .Build();
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            ResolveServices();
        }

        protected IServiceProvider ServiceProvider { get; }
        protected IConfiguration Configuration { get; }

        protected virtual void ConfigureServices(IServiceCollection services) { }
        protected virtual void ResolveServices() { }
    }
}