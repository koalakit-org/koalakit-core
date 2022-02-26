using KoalaKit.Modules;
using KoalaKit.Options;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Caching.InMemory
{
    public class KoalaMemoryCacheModule : KoalaModuleBase
    {
        public override void ConfigureKoala(KoalaOptionsBuilder koala)
        {
            koala.Services.AddSingleton<ICache, MemoryCache>();
            base.ConfigureKoala(koala);
        }
    }
}
