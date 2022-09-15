using KoalaKit.Messaging;
using KoalaKit.Messaging.Queuing;
using KoalaKit.Queuing.RabbitMq.Extensions;
using KoalaKit.Serializations;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Events;

namespace KoalaKit.Queuing.RabbitMq
{
    public class QueuingMessageRabbitMqConsumer<TMessage> : IMessageQueuingConsumer<TMessage>
        where TMessage : IQueuingMessage, new()
    {
        private readonly IMessageQueueFactory<TMessage> queueFactory;
        private readonly IServiceProvider serviceProvider;
        private readonly ISerializer<TMessage> serializer;

        public QueuingMessageRabbitMqConsumer(
            IMessageQueueFactory<TMessage> queueFactory,
            IServiceProvider serviceProvider,
            ISerializer<TMessage> serializer)
        {
            this.queueFactory = queueFactory;
            this.serviceProvider = serviceProvider;
            this.serializer = serializer;
        }


        public void Consume()
        {
            var queue = queueFactory.Create();
            var model = RabbitMqExtensions.InitializeRabbitMqModel(queue);
            model.BasicQos(0, 1, false);
            var consumer = new EventingBasicConsumer(model);
            consumer.Received += Consumer_Received!;
            model.BasicConsume(queue.Name, false, "", noLocal: false, exclusive: false, null, consumer);

        }

        private async void Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var handler = serviceProvider.CreateScope().ServiceProvider.GetService<IMessagingHandler<TMessage>>();
            if (handler != null)
            {
                var message = serializer.Deserialize(eventArgs.Body.ToArray());
                await handler.HandleAsync(message ?? new TMessage());
            }
            ((EventingBasicConsumer)sender).Model.BasicAck(eventArgs.DeliveryTag, false);
        }
    }
}
