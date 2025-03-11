using System.Text;
using KoalaKit.EventBus.Models;
using KoalaKit.EventBus.Services;
using KoalaKit.Primitives.Extensions;
using KoalaKit.Serializations;
using KoalaKit.Tokenizations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace KoalaKit.EventBus.RabbitMQ;

internal sealed class RabbitMQConsumer<TEto>(
    IEventBusQueueFactory<TEto> queueFactory,
    IServiceProvider serviceProvider,
    ILoggerFactory loggerFactory) : IEventBusConsumer<TEto> where TEto : IKoalaEventMessage
{
    private readonly IEventBusQueueFactory<TEto> _queueFactory = queueFactory;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly ILogger _logger = loggerFactory.CreateLogger<RabbitMQConsumer<TEto>>();

    public async Task Consume(CancellationToken cancellationToken)
    {
        var queues = _queueFactory.Create();
        await Task.WhenAll(queues.Select(queue => ConsumeQueueAsync(queue, cancellationToken)));
    }

    private async Task ConsumeQueueAsync(EventBusQueueDefinition queue, CancellationToken cancellationToken)
    {
        try
        {
            var channel = await RabbitMQExtensions.InitializeRabbitMQModel(queue, cancellationToken);
            await channel.BasicQosAsync(0, 1, false, cancellationToken);
            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += Consumer_Received!;

            await channel.BasicConsumeAsync(queue.Name, autoAck: false, consumer: consumer, cancellationToken: cancellationToken);
            _logger.LogInformation("Started consuming from queue {QueueName}", queue.Name);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error initializing consumer for queue {QueueName}: {Error}", queue.Name, ex.Message);
        }
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
    {
        try
        {
            var handler = _serviceProvider.CreateScope().ServiceProvider.GetService<IEtoCosumeHandler<TEto>>();
            if (handler is null)
            {
                _logger.LogWarning("Handler not found for type {Type}", typeof(TEto).Name);
                return;
            }
            var token = Encoding.UTF8.GetString(eventArgs.Body.ToArray());
            var json = token.Detokenize();
            var message = KoalaSerializer.Deserialize<TEto>(json);
            if (message != null)
            {
                var doAck = await handler.HandleAsync(message, eventArgs.CancellationToken);
                if (doAck)
                {
                    await ((AsyncEventingBasicConsumer)sender).Channel.BasicAckAsync(eventArgs.DeliveryTag, false);
                }
                else
                {
                    await ((AsyncEventingBasicConsumer)sender).Channel.BasicNackAsync(eventArgs.DeliveryTag, false, true);
                }
            }
        }
        catch (Exception exception)
        {
            _logger.LogError("Error processing message for type {Type}. Details: {Details}", typeof(TEto).Name, exception.ExtractData());
            await ((AsyncEventingBasicConsumer)sender).Channel.BasicNackAsync(eventArgs.DeliveryTag, false, true);
        }
    }
}