using Microsoft.Extensions.Caching.Memory;

namespace KoalaKit.Caching.Memory
{
    internal class KoalaMemoryCache : ICache
    {
        private readonly IMemoryCache memoryCache;

        public KoalaMemoryCache(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            memoryCache.TryGetValue<T>(key, out var value);
            return value;
        }

        public async Task SetAsync<T>(string key, T data)
        {
            memoryCache.Set(key, data);
        }
    }
}
