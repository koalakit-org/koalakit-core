namespace KoalaKit.Caching
{
    public interface ICacheFactory
    {
        ICache GetOrCreate<T>();
        void AddProvider(ICacheProvider provider);
    }

}
