namespace KoalaKit.Caching
{
    public interface ICache<T>
    {
        T Get(string key);
        void Set(string key, T value);
    }

}
