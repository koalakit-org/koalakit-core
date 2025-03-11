using KoalaKit.EventBus.Extensions;
using KoalaKit.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.EventBus.RabbitMQ;

public sealed class RabbitMQConsumingModule : KoalaModuleBase
{
    public override void ConfigureKoala(KoalaContext koala)
    {
        koala.AddKoalaEventsBusCore();
        koala.Services.AddSingleton(typeof(IEventBusConsumer<>), typeof(RabbitMQConsumer<>));

        koala.Services.AddTransient(typeof(IEventBusPublisher<>), typeof(RabbitMQPublisher<>));
        koala.Services.AddScoped<IEventBusPublisher, RabbitMQPublisher>();
        base.ConfigureKoala(koala);
    }
}