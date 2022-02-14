using KoalaKit.Options;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKoalaKit(this IServiceCollection services, Action<KoalaOptionsBuilder> configure)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"appsettings.{new HostingEnvironment().EnvironmentName}.json", optional: true, reloadOnChange: true)
                .Build();

            return services.AddKoalaKit(configuration, configure);
        }

        public static IServiceCollection AddKoalaKit(this IServiceCollection services,IConfiguration configuration, Action<KoalaOptionsBuilder> configure)
        {
            var koalaBuilder = new KoalaOptionsBuilder(services, configuration);
            configure.Invoke(koalaBuilder);

            services.AddSingleton(koalaBuilder.Options);

            return services;
        }
    }
}
