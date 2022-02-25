using Microsoft.Extensions.Caching.Memory;

namespace KoalaKit.Caching.InMemory
{
    public class MemoryCache : ICache
    {
        private readonly Dictionary<string, CacheItem>

        public MemoryCache(IMemoryCache cache)
        {
            this.cache = cache;
        }
        public async Task SetAsync<T>(string key, T data)
        {
            cache.Set<T>(key, data);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return cache.Get<T>(key);
        }
    }
}
