namespace KoalaKit.Caching
{
    public class KoalaCache<T> : ICache<T>
    {
        private readonly ICache<T> cache;

        public KoalaCache(ICacheFactory cacheFactory)
        {
            this.cache = cacheFactory.Create<T>();
        }

        public T Get(string key)
            => cache.Get(key);

        public void Set(string key, T value)
            => cache.Set(key, value);
    }
}
