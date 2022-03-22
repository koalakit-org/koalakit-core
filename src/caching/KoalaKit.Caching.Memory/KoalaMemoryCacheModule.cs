using KoalaKit.Modules;
using KoalaKit.Options;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Caching.Memory
{
    public class KoalaMemoryCacheModule : KoalaModuleBase
    {
        public override void ConfigureKoala(KoalaOptionsBuilder koala)
        {
            koala.AddKoalaCachingCore();
            koala.Services.AddMemoryCache();
            koala.Services.AddScoped<KoalaMemoryCache>();
            koala.Services.AddScoped(typeof(ICacheProvider<>), typeof(MemoryCacheProvider<>));
            base.ConfigureKoala(koala);
        }
    }
}