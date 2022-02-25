using KoalaKit.Options;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Caching.InMemory
{
    public class MemoryCacheProvider : ICacheProvider
    {
        public MemoryCacheProvider(KoalaOptionsBuilder koala)
        {
        }

        public ICache CreateCache()
        {
            var service = new MemoryCache();
            if (service == null)
                throw new InvalidOperationException();
            return (ICache)service;
        }
    }
}
