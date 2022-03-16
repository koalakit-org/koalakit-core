using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<T> GetAsync<T>(string key)
        {
            if (memoryCache.TryGetValue<T>(key, out var value))
                return value;

            return default(T);
        }

        public async Task SetAsync<T>(string key, T data)
        {
            memoryCache.Set(key, data);
        }
    }
}
