using KoalaKit.Options;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Caching
{
    public static class KoalaOptionsBuilderExtensions
    {
        public static KoalaOptionsBuilder AddKoalaCachingCore(this KoalaOptionsBuilder koala)
        {
            koala.Services.AddScoped<ICache, Cache>();
            koala.Services.AddScoped<ICacheFactory, CacheFactory>();
            return koala;
        }
    }
}
