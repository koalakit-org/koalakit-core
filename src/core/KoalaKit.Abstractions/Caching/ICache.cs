namespace KoalaKit.Caching
{
    public interface ICache
    {
        Task SetAsync<T>(T data, string key);
        Task<T> GetAsync<T>(string key);
    }
}
