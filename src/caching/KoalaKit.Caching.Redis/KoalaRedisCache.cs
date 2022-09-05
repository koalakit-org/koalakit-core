using System.Text.Json;
using KoalaKit.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace Koalakit.Caching.Redis
{
    public class KoalaRedisCache : ICache
    {
        private readonly IDistributedCache cache;
        public KoalaRedisCache(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var jsonData = await cache.GetStringAsync(key);
            if (jsonData is null) return default(T);
            return JsonSerializer.Deserialize<T>(jsonData);
        }

        public async Task SetAsync<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(900),
                SlidingExpiration = TimeSpan.FromSeconds(90),
            };
            await cache.SetStringAsync(key, JsonSerializer.Serialize(data), options);
        }
    }
}