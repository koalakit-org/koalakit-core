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

        protected IServiceProvider ServiceProvider { get; private set; }
        protected IConfiguration Configuration { get; private set; }

        protected virtual void ConfigureServices(IServiceCollection services) { }
        protected virtual void ResolveServices() { }
    }
}