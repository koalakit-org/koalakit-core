using Microsoft.Extensions.Caching.Memory;

namespace KoalaKit.Caching.InMemory
{
    internal class KoalaMemoryCache<T> : ICache<T>
    {
        private readonly Dictionary<string, T> cache = new();
        public T Get(string key)
        {
            return cache.ContainsKey(key) ? cache[key] : default(T);
        }

        public void Set(string key, T value)
        {
            cache[key] = value;
        }
    }
}
