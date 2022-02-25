namespace KoalaKit.Caching
{
    public class CacheFactory : ICacheFactory
    {
        private readonly Dictionary<string, ICache> caches = new(StringComparer.Ordinal);

        public ICache GetOrCreate<T>()
            => GetOrCreate(nameof(T));

        public ICache GetOrCreate(string category)
        {
            if (caches.TryGetValue(category, out var cache)) return cache;
            
            cache = CacheProviderStorage.GetProvider(category).CreateCache();
            caches[category] = cache;
            return cache;
        }
    }
}
