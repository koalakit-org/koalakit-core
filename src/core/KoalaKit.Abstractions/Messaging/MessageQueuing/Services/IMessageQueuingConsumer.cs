namespace KoalaKit.Messaging
{
    public interface IMessageQueuingConsumer<in TMessage> where TMessage : IQueuingMessage, new()
    {
        public void Consume();
    }
}
