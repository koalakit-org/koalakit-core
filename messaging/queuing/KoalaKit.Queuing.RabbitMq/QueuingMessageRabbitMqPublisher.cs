using KoalaKit.Messaging.Queuing;
using KoalaKit.Queuing.RabbitMq.Extensions;
using KoalaKit.Serializations;
using RabbitMQ.Client;

namespace KoalaKit.Queuing.RabbitMq
{
    public class QueuingMessageRabbitMqPublisher<TMessage> : IMessageQueuingPublisher<TMessage> where TMessage : IQueuingMessage, new()
    {
        private readonly IMessageQueueFactory<TMessage> queueFactory;
        private readonly ISerializer<TMessage> serializer;

        public QueuingMessageRabbitMqPublisher(
            IMessageQueueFactory<TMessage> queueFactory,
            ISerializer<TMessage> serializer)
        {
            this.queueFactory = queueFactory;
            this.serializer = serializer;
        }

        public void Publish(TMessage message)
        {
            var queue = queueFactory.Create(message);
            var model = RabbitMqExtensions.InitializeRabbitMqModel(queue, false);
            model.BasicPublish(queue.Connection.Exchange, queue.Route, model.CreateBasicProperties(), serializer.Serialize(message));
        }
    }
}
