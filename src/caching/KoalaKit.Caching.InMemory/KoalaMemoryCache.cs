using Microsoft.Extensions.Caching.Memory;

namespace KoalaKit.Caching.InMemory
{
    //Inprogress. assumed as not implemented.
    internal class KoalaMemoryCache<T> : ICache<T>
    {
        private readonly IMemoryCache cache;

        public KoalaMemoryCache(IMemoryCache cache)
        {
            this.cache = cache;
        }

        public T Get(string key)
        {
            return cache.TryGetValue(key, out var value) ? (T) value : default(T);
        }

        public void Set(string key, T value)
        {
            cache.Set(key, value);
        }
    }
}
