using KoalaKit.Messaging;
using KoalaKit.Messaging.Bus;
using KoalaKit.Serializations;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace KoalaKit.Bus.Redis
{
    public class RedisSubChannel<TMessage> : ISubChannel<TMessage>
        where TMessage : IBusMessage, new()
    {
        private readonly IConnectionMultiplexer multiplexer;
        private readonly IBusMessageDefinitionFactory<TMessage> definitionFactory;
        private readonly ISerializer<TMessage> serializer;
        private readonly IServiceProvider serviceProvider;

        public RedisSubChannel(
            IConnectionMultiplexer multiplexer,
            IBusMessageDefinitionFactory<TMessage> definitionFactory,
            ISerializer<TMessage> serializer,
            IServiceProvider serviceProvider)
        {
            this.multiplexer = multiplexer;
            this.definitionFactory = definitionFactory;
            this.serializer = serializer;
            this.serviceProvider = serviceProvider;
        }


        public async Task SubscribeAsync()
        {
            await multiplexer.GetSubscriber().SubscribeAsync(definitionFactory.Create().Channel, Handler);
        }

        public async Task SubscribeAsync(Action<TMessage> handler)
        {
            var messageBus = definitionFactory.Create();
            await multiplexer.GetSubscriber().SubscribeAsync(messageBus.Channel, (_, redisValue) =>
            {
                var message = serializer.Deserialize(redisValue);
                if(message == null) return;

                handler.Invoke(message);
            });
        }

        public async Task UnsubscribeAsync() => await multiplexer.GetSubscriber().UnsubscribeAsync(definitionFactory.Create().Channel);


        private async void Handler(RedisChannel channel, RedisValue redisValue)
        {
            var message = serializer.Deserialize(redisValue);
            if (message == null) return;

            var handler = serviceProvider.CreateScope().ServiceProvider.GetService<IMessagingHandler<TMessage>>();
            if (handler != null)
                await handler.HandleAsync(message);
        }
    }
}
