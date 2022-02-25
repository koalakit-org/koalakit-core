namespace KoalaKit.Caching
{
    public static class CacheProviderStorage
    {
        private const string DefaultCategory = "Category";
        private static readonly List<CacheProviderRegistration> ProviderRegistrations = new();

        public static void AddProvider(ICacheProvider provider)
            => AddProviderRegistration(provider, DefaultCategory);

        public static void AddProvider<T>(ICacheProvider provider)
            => AddProviderRegistration(provider, nameof(T));

        public static ICacheProvider GetProvider(string category)
        {
            var providerRegistration = ProviderRegistrations.FirstOrDefault(a => a.Category == category);

            if (string.IsNullOrWhiteSpace(providerRegistration.Category))
                providerRegistration = ProviderRegistrations.First(a => a.Category == DefaultCategory);

            return providerRegistration.Provider;
        }

        private static void AddProviderRegistration(ICacheProvider provider, string category)
        {
            ProviderRegistrations.Add(new CacheProviderRegistration
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
