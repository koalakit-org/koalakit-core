using KoalaKit.EventBus.Models;
using RabbitMQ.Client;

namespace KoalaKit.EventBus.RabbitMQ;

internal static class RabbitMQExtensions
{
    private static readonly Dictionary<string, IChannel> Connections = [];
    private static string CombineKey(EventBusQueueDefinition definition) => $"{definition.Connection.Host}{definition.Connection.VirtualHost}{definition.Name}";

    internal static async Task<IChannel> CreateChannel(this EventBusConnectionDefinition definition, CancellationToken cancellationToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = definition.Host,
            VirtualHost = definition.VirtualHost,
            UserName = definition.UserName,
            Password = definition.Password
        };
        var connection = await factory.CreateConnectionAsync(cancellationToken);
        var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);
        return channel;
    }

    internal static void CloseChannel(this EventBusQueueDefinition definition)
    {
        var key = CombineKey(definition);
        if (!Connections.TryGetValue(key, out IChannel? value))
        {
            return;
        }
        var channel = value;
        channel.Dispose();
        Connections.Remove(key);
    }

    internal static void CloseAllChannels()
    {
        foreach (var connection in Connections)
        {
            connection.Value.Dispose();
        }
        Connections.Clear();
    }

    internal static async Task<IChannel> InitializeRabbitMQModel(EventBusQueueDefinition definition, CancellationToken cancellationToken)
    {
        if (Connections.ContainsKey(CombineKey(definition)))
            return Connections[CombineKey(definition)];

        var model = await definition.Connection.CreateChannel(cancellationToken);
        await model.ExchangeDeclareAsync(definition.Connection.Exchange, ExchangeType.Topic, durable: true, autoDelete: false, cancellationToken: cancellationToken);
        await model.QueueDeclareAsync(definition.Name, durable: true, exclusive: false, autoDelete: false, arguments: null, cancellationToken: cancellationToken);
        await model.QueueBindAsync(definition.Name, definition.Connection.Exchange, definition.Route, cancellationToken: cancellationToken);

        Connections.TryAdd(CombineKey(definition), model);
        return model;
    }
}