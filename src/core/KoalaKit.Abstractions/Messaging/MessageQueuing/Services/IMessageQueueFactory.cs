namespace KoalaKit.Messaging
{
    public interface IMessageQueueFactory<in TMessage>
        where TMessage : IQueuingMessage, new()
    {
        MessageQueueDefinition Create();
        MessageQueueDefinition Create(TMessage message);
    }
}
