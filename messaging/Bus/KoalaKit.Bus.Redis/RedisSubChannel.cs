using KoalaKit.Messaging.Bus;

namespace KoalaKit.Bus.Redis
{
    public class RedisSubChannel<TMessage> : ISubChannel<TMessage> where TMessage : class, IBusMessage
    {
        public Task SubscribeAsync()
        {
            throw new NotImplementedException();
        }

        public Task UnsubscribeAsync()
        {
            throw new NotImplementedException();
        }
    }
}
