namespace KoalaKit.Caching
{
    public interface ICacheProvider<T>
    {
        ICache Get();
    }
}
