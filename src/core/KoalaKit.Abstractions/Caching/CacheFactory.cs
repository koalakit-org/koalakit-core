namespace KoalaKit.Caching
{
    public class CacheFactory : ICacheFactory
    {
        private const string DefaultCategory = "Category";
        private readonly Dictionary<string, ICache> caches = new(StringComparer.Ordinal);
        private readonly List<CacheProviderRegistration> providerRegistrations = new();

        public ICache GetOrCreate<T>()
            => GetOrCreate(nameof(T));

        public ICache GetOrCreate(string category)
        {
            if (caches.TryGetValue(category, out var cache)) return cache;
            
            cache = GetProvider(category).CreateCache();
            caches[category] = cache;
            return cache;
        }


        public void AddProvider(ICacheProvider provider)
        {
            AddProviderRegistration(provider, DefaultCategory);
        }

        public void AddProvider<T>(ICacheProvider provider)
        {
            AddProviderRegistration(provider, nameof(T));
        }

        private ICacheProvider GetProvider(string category)
        {
            var providerRegistration = providerRegistrations.FirstOrDefault(a => a.Category == category);

            if (providerRegistration.Provider == null || string.IsNullOrWhiteSpace(providerRegistration.Category))
                providerRegistration = providerRegistrations.First(a => a.Category == DefaultCategory);

            return providerRegistration.Provider;
        }
        private void AddProviderRegistration(ICacheProvider provider, string category)
        {
            providerRegistrations.Add(new CacheProviderRegistration
            {
                Category = category,
                Provider = provider
            });
        }

        private struct CacheProviderRegistration
        {
            public ICacheProvider Provider;
            public string Category;
        }
    }
}
