namespace KoalaKit.Caching
{
    internal class Cache : ICache
    {
        private readonly ICacheFactory cacheFactory;

        public Cache(ICacheFactory cacheFactory)
        {
            this.cacheFactory = cacheFactory;
        }

        public async Task SetAsync<T>(string key, T data)
            => await cacheFactory.GetOrCreate<T>().SetAsync(key, data);

        public async Task<T> GetAsync<T>(string key)
            => await cacheFactory.GetOrCreate<T>().GetAsync<T>(key);
    }
}
