namespace KoalaKit.Caching
{
    public interface ICache<T>
    {
        T Get(string key) => throw new NotImplementedException();

        void Set(string key, T value) => throw new NotImplementedException();
    }

}
