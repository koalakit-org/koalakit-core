namespace KoalaKit.Caching.Memory
{
    public class MemoryCacheProvider<T> : ICacheProvider<T>
    {
        private readonly ICache cache;
        public ICache Get()
        {
            return cache;
        }
    }
}
