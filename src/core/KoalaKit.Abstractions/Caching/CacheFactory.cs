using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Caching
{
    public class CacheFactory : ICacheFactory
    {
        private readonly IServiceProvider serviceProvider;

        public CacheFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public ICache Create<T>()
        {
            var provider = serviceProvider.CreateScope().ServiceProvider.GetService<ICacheProvider<T>>();
            if (provider == null) throw new InvalidOperationException("No cache registred!");
            return provider.Get();
        }
    }
}
