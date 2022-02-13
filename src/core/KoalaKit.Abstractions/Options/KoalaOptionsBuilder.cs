using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Options
{
    public class KoalaOptionsBuilder
    {
        public KoalaOptionsBuilder(IServiceCollection services)
        {
            KoalaOptions = new KoalaOptions();
            Services = services;
        }

        public KoalaOptions KoalaOptions { get; }
        public IServiceCollection Services { get; }
    }
}
