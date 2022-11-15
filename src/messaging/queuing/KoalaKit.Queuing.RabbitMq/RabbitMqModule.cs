using KoalaKit.Modules;
using KoalaKit.Options;
using KoalaKit.Queuing.RabbitMq.Extensions;

namespace KoalaKit.Queuing.RabbitMq
{
    public class RabbitMqModule : KoalaModuleBase
    {
        public override void ConfigureKoala(KoalaOptionsBuilder koala)
        {
            koala.UseRabbitMq();
            base.ConfigureKoala(koala);
        }
    }
}