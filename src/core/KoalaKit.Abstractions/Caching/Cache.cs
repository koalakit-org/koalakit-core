namespace KoalaKit.Caching
{

    //// is correct to implement list cach items
    public class Cache : ICache
    {
        private readonly ICacheFactory cacheFactory;
        public Cache(ICacheFactory cacheFactory) => this.cacheFactory = cacheFactory;
        public async Task<T> GetAsync<T>(string key) => await cacheFactory.Create<T>().GetAsync<T>(key);
        public async Task SetAsync<T>(string key, T data)
        {
            var cache = cacheFactory.Create<T>();
            await cache.SetAsync(key, data);
        }
    }
}
