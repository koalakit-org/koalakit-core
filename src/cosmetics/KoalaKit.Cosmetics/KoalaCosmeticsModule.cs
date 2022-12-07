using KoalaKit.Modules;
using KoalaKit.Options;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Cosmetics
{
    public class KoalaCosmeticsModule : KoalaModuleBase
    {
        public override void ConfigureKoala(KoalaOptionsBuilder koala)
        {
            koala.Services.AddScoped<ITokenizationService, TokenizationService>();
            koala.Services.AddDataProtection();
            base.ConfigureKoala(koala);
        }
    }
}
