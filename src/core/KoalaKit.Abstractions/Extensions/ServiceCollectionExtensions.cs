using KoalaKit.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKoalaKit(this IServiceCollection services,IConfiguration configuration, Action<KoalaOptionsBuilder> configure)
        {
            var koalaBuilder = new KoalaOptionsBuilder(services, configuration);
            configure.Invoke(koalaBuilder);

            services.AddSingleton(koalaBuilder.Options);

            return services;
        }
    }
}
