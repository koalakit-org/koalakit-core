using KoalaKit.Messaging.Bus;
using KoalaKit.Serializations;
using StackExchange.Redis;

namespace KoalaKit.Bus.Redis
{
    public class RedisSubChannel<TMessage> : ISubChannel<TMessage> where TMessage : class, IBusMessage
    {
        private readonly IConnectionMultiplexer multiplexer;
        private readonly IBusMessageDefinitionFactory<TMessage> definitionFactory;
        private readonly ISerializer<TMessage> serializer;

        public RedisSubChannel(
            IConnectionMultiplexer multiplexer,
            IBusMessageDefinitionFactory<TMessage> definitionFactory,
            ISerializer<TMessage> serializer)
        {
            this.multiplexer = multiplexer;
            this.definitionFactory = definitionFactory;
            this.serializer = serializer;
        }


        public async Task SubscribeAsync()
        {
            var messageBus = definitionFactory.Create();
            await multiplexer.GetSubscriber().SubscribeAsync(messageBus.Channel, (channel, redisValue) =>
            {
                //implement the handlers
            });
        }

        public async Task SubscribeAsync(Action<TMessage> handler)
        {
            var messageBus = definitionFactory.Create();
            await multiplexer.GetSubscriber().SubscribeAsync(messageBus.Channel, (channel, redisValue) =>
            {
                var message = serializer.Deserialize(redisValue);
                if(message == null) return;

                handler.Invoke(message);
            });
        }

        public async Task UnsubscribeAsync() => await multiplexer.GetSubscriber().UnsubscribeAsync(definitionFactory.Create().Channel);
    }
}
