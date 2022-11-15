namespace KoalaKit.Messaging.Queuing
{
    public interface IMessageQueuingConsumer<in TMessage>
        where TMessage : IQueuingMessage, new()
    {
        public void Consume();
    }
}
