using KoalaKit.Messaging.Bus;
using KoalaKit.Serializations;
using StackExchange.Redis;

namespace KoalaKit.Bus.Redis
{
    public class RedisPubChannel<TMessage> : IPubChannel<TMessage> where TMessage : class, IBusMessage
    {
        private readonly IConnectionMultiplexer multiplexer;
        private readonly IBusMessageDefinitionFactory<TMessage> definitionFactory;
        private readonly ISerializer<TMessage> serializer;

        public RedisPubChannel(
            IConnectionMultiplexer multiplexer,
            IBusMessageDefinitionFactory<TMessage> definitionFactory,
            ISerializer<TMessage> serializer)
        {
            this.multiplexer = multiplexer;
            this.definitionFactory = definitionFactory;
            this.serializer = serializer;
        }
        public async Task PublishAsync(TMessage message)
        {
            var messageBus = definitionFactory.Create(message);
            await multiplexer.GetSubscriber().PublishAsync(messageBus.Channel, serializer.Serialize(message)); 
        }
     }
}