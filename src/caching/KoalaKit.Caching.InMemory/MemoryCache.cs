using Microsoft.Extensions.Caching.Memory;

namespace KoalaKit.Caching.InMemory
{
    public class MemoryCache : ICache
    {
        private readonly IMemoryCache memoryCache;
        public async Task SetAsync<T>(string key, T data)
        {
            memoryCache.Set<T>(key, data);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return memoryCache.Get<T>(key);
        }
    }
}
