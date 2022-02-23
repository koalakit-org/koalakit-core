namespace KoalaKit.Caching
{
    public class CacheFactory : ICacheFactory
    {
        private readonly Dictionary<string, ICache> caches = new(StringComparer.Ordinal);
        private readonly List<CacheProviderRegistration> providerRegistrations = new();
        public ICache GetOrCreate<T>()
        {
            if (caches.TryGetValue("", out var cache)) return cache;

            var provider = providerRegistrations
                .First(a => a.CachedType == typeof(string)).Provider;
            cache = provider.CreateCache();
            caches[""] = cache;
            return cache;
        }

        public void AddProvider(ICacheProvider provider)
        {
            AddProviderRegistration(provider, typeof(string));
        }

        private void AddProviderRegistration(ICacheProvider provider, Type cachedType)
        {
            providerRegistrations.Add(new CacheProviderRegistration
            {
                CachedType = cachedType,
                Provider = provider
            });
        }

        private struct CacheProviderRegistration
        {
            public ICacheProvider Provider;
            public Type CachedType;
        }
    }
}
