using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Options
{
    public class KoalaOptionsBuilder
    {
        public KoalaOptionsBuilder(IServiceCollection services, IConfiguration configuration)
        {
            Configuration = configuration;
            Options = new KoalaOptions();
            Services = services;
        }

        public IConfiguration Configuration;
        public KoalaOptions Options { get; }
        public IServiceCollection Services { get; }
    }
}
