using KoalaKit.Messaging.Bus;

namespace KoalaKit.Bus.Redis
{
    public class RedisPubChannel<TMessage> : IPubChannel<TMessage> where TMessage : class, IBusMessage
    {
        public Task PublishAsync(TMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
