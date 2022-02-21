namespace KoalaKit.Caching
{
    public interface ICacheFactory
    {
        ICache<T> Create<T>();
    }
}
