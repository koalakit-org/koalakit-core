using KoalaKit.Options;
using KoalaKit.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddKoalaKitCore(this IServiceCollection services, Action<KoalaOptionsBuilder> configure)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .AddJsonFile($"appsettings.{new HostingEnvironment().EnvironmentName}.json", optional: true, reloadOnChange: true)
                .Build();

            return services.AddKoalaKitCore(configuration, configure);
        }

        public static IServiceCollection AddKoalaKitCore(this IServiceCollection services,IConfiguration configuration, Action<KoalaOptionsBuilder> configure)
        {
            var koalaBuilder = new KoalaOptionsBuilder(services, configuration);
            configure.Invoke(koalaBuilder);

            services.AddSingleton(koalaBuilder.Options);

            return services;
        }

        public static IServiceCollection AddKoalaTask<TTask>(this IServiceCollection services)
            where TTask : class, IKoalaTask
        {
            return services
                .AddScoped<TTask>()
                .AddScoped<IKoalaTask, TTask>(sp => sp.GetRequiredService<TTask>());
        }
    }
}
