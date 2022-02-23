namespace KoalaKit.Caching
{
    public interface ICache
    {
        Task SetAsync<T>(string key, T data);
        Task<T> GetAsync<T>(string key);
    }
}
