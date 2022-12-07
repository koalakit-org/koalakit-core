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
                //.UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration
                //{
                //    EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                //    ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                //});
            base.ConfigureKoala(koala);
        }
    }
}
