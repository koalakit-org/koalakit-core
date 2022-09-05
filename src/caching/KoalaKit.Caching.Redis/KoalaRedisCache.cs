using KoalaKit.Caching;

namespace Koalakit.Caching.Redis
{
    public class KoalaRedisCache : ICache
    {
        public Task<T?> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task SetAsync<T>(string key, T data)
        {
            throw new NotImplementedException();
        }
    }
}