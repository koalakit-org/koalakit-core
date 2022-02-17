namespace KoalaKit.Messaging.Queuing
{
    public interface IMessageQueueFactory<in TMessage>
        where TMessage : IQueuingMessage, new()
    {
        MessageQueueDefinition Create();
        MessageQueueDefinition Create(TMessage message);
    }
}
