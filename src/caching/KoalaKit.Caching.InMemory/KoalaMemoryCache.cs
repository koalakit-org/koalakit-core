using Microsoft.Extensions.Caching.Memory;

namespace KoalaKit.Caching.InMemory
{
    internal class KoalaMemoryCache<T> : ICache<T>
    {

        public KoalaMemoryCache(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }


        public T Get(string key)
        {
            if(memoryCache.TryGetValue(key, out var v))
                return (T)v;
            
            string timestamp = memoryCache.GetOrCreate(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5);
                return DateTime.Now.ToString();
            });

            //memoryCache.GetOrCreateAsync()

        }

        public void Set(string key, T value)
        {
            var entry = memoryCache.CreateEntry(key);
            entry.Value = value;

            entry.
        }
    }

    public class KoalaMemoryCache : IMemoryCache
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(object key, out object value)
        {
            throw new NotImplementedException();
        }

        public ICacheEntry CreateEntry(object key)
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }
    }
}
