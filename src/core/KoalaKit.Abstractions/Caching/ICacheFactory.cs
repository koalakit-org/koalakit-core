using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Caching
{
    public interface ICacheFactory
    {
        ICache Create<T>();
    }
}
