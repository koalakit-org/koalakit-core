namespace KoalaKit.Messaging.Queuing
{
    public class DefaultMessageQueueFactory<TMessage> : IMessageQueueFactory<TMessage> where TMessage : IQueuingMessage, new()
    {
        private readonly string queueName = new TMessage().QueueName;
        private readonly IMessageQueuingConnectionSelector<TMessage> connectionSelector;

        public DefaultMessageQueueFactory(IMessageQueuingConnectionSelector<TMessage> connectionSelector) => this.connectionSelector = connectionSelector;


        public MessageQueueDefinition Create() => new MessageQueueDefinition(queueName, queueName, connectionSelector.Select(queueName));
        public MessageQueueDefinition Create(TMessage message) => new MessageQueueDefinition(message.QueueName, message.QueueName, connectionSelector.Select());
        
    }
}