using KoalaKit.Caching;
using Microsoft.Extensions.DependencyInjection;

namespace Koalakit.Caching.Redis
{
    public class KoalaRedisCacheProvider<T> : ICacheProvider<T>
    {

        private readonly IServiceProvider serviceProvider;

        public KoalaRedisCacheProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICache Get()
        {
            var cache = serviceProvider.CreateScope().ServiceProvider.GetService<KoalaRedisCache>();
            if (cache == null) throw new InvalidOperationException();
            return cache;
        }
    }
}
