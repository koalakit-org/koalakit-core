namespace KoalaKit.Caching
{
    internal class Cache : ICache
    {
        public Task SetAsync<T>(T data, string key)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }
    }
}
