using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Options
{
    public class KoalaOptionsBuilder
    {
        public KoalaOptionsBuilder(IServiceCollection services, IConfiguration configuration)
        {
            Configuration = configuration;
            KoalaOptions = new KoalaOptions();
            Services = services;
        }

        public IConfiguration Configuration;
        public KoalaOptions KoalaOptions { get; }
        public IServiceCollection Services { get; }
    }
}
