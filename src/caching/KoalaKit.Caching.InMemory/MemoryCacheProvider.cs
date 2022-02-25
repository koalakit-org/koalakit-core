using Microsoft.Extensions.Caching.Memory;

namespace KoalaKit.Caching.InMemory
{
    public class MemoryCacheProvider : ICacheProvider
    {
        public ICache CreateCache()
        {
            return new MemoryCache();
        }
    }
}
