using KoalaKit.EventBus.Models;
using KoalaKit.EventBus.Services;
using KoalaKit.Serializations;
using KoalaKit.Tokenizations;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace KoalaKit.EventBus.RabbitMQ;

internal sealed class RabbitMQPublisher<TEto>(IEventBusQueueFactory<TEto> queueFactory) : IEventBusPublisher<TEto>
    where TEto : IKoalaEventMessage
{
    readonly IEventBusQueueFactory<TEto> _queueFactory = queueFactory;

    public async Task Publish(TEto eto, CancellationToken cancellationToken = default)
    {
        var queue = _queueFactory.Create(eto);
        var model = await RabbitMQExtensions.InitializeRabbitMQModel(queue, cancellationToken);
        var token = KoalaSerializer.ToJson(eto).Tokenize();
        var etoBody = KoalaSerializer.ToBinary(token);

        var props = new BasicProperties();
        await model.BasicPublishAsync(queue.Connection.Exchange, queue.Route, false, props, etoBody, cancellationToken);
    }
}

internal sealed class RabbitMQPublisher(IServiceScopeFactory scopeFactory) : IEventBusPublisher
{
    readonly IServiceScopeFactory _scopeFactory = scopeFactory;
    public async Task Publish<TEto>(TEto eto, CancellationToken cancellationToken = default)
        where TEto : IKoalaEventMessage
    {
        using var scope = _scopeFactory.CreateScope();
        var queueFactory = scope.ServiceProvider.GetRequiredService<IEventBusQueueFactory<TEto>>();
        var queue = queueFactory.Create(eto);
        var model = await RabbitMQExtensions.InitializeRabbitMQModel(queue, cancellationToken);
        var token = KoalaSerializer.ToJson(eto).Tokenize();
        var etoBody = KoalaSerializer.ToBinary(token);
        var props = new BasicProperties();
        await model.BasicPublishAsync(queue.Connection.Exchange, queue.Route, false, props, etoBody, cancellationToken);
    }
}