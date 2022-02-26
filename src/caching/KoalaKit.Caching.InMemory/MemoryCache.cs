using Microsoft.Extensions.Caching.Memory;

namespace KoalaKit.Caching.InMemory
{
    public class MemoryCache : ICache
    {
        public Task SetAsync<T>(string key, T data)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }
    }
}
