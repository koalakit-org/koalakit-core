namespace KoalaKit.Messaging.Queuing
{
    public interface IMessageQueuingPublisher<in TMessage> where TMessage : IQueuingMessage, new()
    {
        public void Publish(TMessage message);
    }
}
