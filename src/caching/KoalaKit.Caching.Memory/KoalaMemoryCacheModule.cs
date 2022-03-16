using KoalaKit.Modules;
using KoalaKit.Options;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Caching.Memory
{
    internal class KoalaMemoryCacheModule : KoalaModuleBase
    {
        public override void ConfigureKoala(KoalaOptionsBuilder koala)
        {
            koala.AddKoalaCachingCore();
            koala.Services.AddScoped<ICacheProvider, MemoryCacheProvider>();
            base.ConfigureKoala(koala);
        }
    }
}