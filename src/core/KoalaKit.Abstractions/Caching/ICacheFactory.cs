namespace KoalaKit.Caching
{
    public interface ICacheFactory
    {
        ICache GetOrCreate<T>();
        ICache GetOrCreate(string cacheCategory);
    }
}
