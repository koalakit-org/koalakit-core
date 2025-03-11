using KoalaKit.EventBus.Extensions;
using KoalaKit.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.EventBus.RabbitMQ;

public sealed class RabbitMQModule : KoalaModuleBase
{
    public override void ConfigureKoala(KoalaContext koala)
    {
        koala.AddKoalaEventsBusCore();
        koala.Services.AddTransient(typeof(IEventBusPublisher<>), typeof(RabbitMQPublisher<>));
        base.ConfigureKoala(koala);
    }
}