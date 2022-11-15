using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Caching.Memory
{
    public class KoalaMemoryCacheProvider<T> : ICacheProvider<T>
    {
        private readonly IServiceProvider serviceProvider;

        public KoalaMemoryCacheProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICache Get()
        {
            var cache = serviceProvider.CreateScope().ServiceProvider.GetService<KoalaMemoryCache>();
            if (cache == null) throw new InvalidOperationException();
            return cache;
        }
    }
}
