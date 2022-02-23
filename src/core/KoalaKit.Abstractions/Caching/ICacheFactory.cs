namespace KoalaKit.Caching
{
    public interface ICacheFactory
    {
        ICache GetOrCreate<T>();
        ICache GetOrCreate(string cacheCategory);
        void AddProvider(ICacheProvider provider);
        void AddProvider<T>(ICacheProvider provider);
    }

}
