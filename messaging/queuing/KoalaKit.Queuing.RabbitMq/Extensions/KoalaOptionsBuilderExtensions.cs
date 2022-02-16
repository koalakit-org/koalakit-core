using KoalaKit.Messaging;
using KoalaKit.Options;
using Microsoft.Extensions.DependencyInjection;

namespace KoalaKit.Queuing.RabbitMq.Extensions
{
    internal static class KoalaOptionsBuilderExtensions
    {
        internal static KoalaOptionsBuilder UseRabbitMq(this KoalaOptionsBuilder koala)
        {
            koala.AddMessageQueuingCore();
            koala.Services.AddScoped(typeof(IMessageQueuingPublisher<>), typeof(QueuingMessageRabbitMqPublisher<>));
            koala.Services.AddSingleton(typeof(IMessageQueuingConsumer<>), typeof(QueuingMessageRabbitMqConsumer<>));
            return koala;
        }
    }
}
