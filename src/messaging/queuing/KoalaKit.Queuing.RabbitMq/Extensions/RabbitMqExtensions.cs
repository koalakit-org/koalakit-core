using KoalaKit.Messaging.Queuing;
using RabbitMQ.Client;

namespace KoalaKit.Queuing.RabbitMq.Extensions
{
    internal static class RabbitMqExtensions
    {
        private static readonly Dictionary<string, IModel> Connections = new();
        private static string CombineKey(MessageQueueDefinition definition) => $"{definition.Connection.Host}{definition.Connection.VirtualHost}{definition.Name}";
        internal static IModel CreateConnection(MessageQueuingConnectionDefinition connectionDefinition)
        {
            var connection = new ConnectionFactory()
            {
                HostName = connectionDefinition.Host,
                VirtualHost = connectionDefinition.VirtualHost,
                Port = connectionDefinition.Port,
                UserName = connectionDefinition.Username,
                Password = connectionDefinition.Password
            }.CreateConnection();

            var channel = connection.CreateModel();
            return channel;
        }


        internal static IModel InitializeRabbitMqModel(MessageQueueDefinition definition, bool checkifExists = true)
        {
            if (checkifExists)
            {
                if (Connections.ContainsKey(CombineKey(definition)))
                    return Connections[CombineKey(definition)];
            }


            /* 
             * durable: Should this queue will survive a broker restart?
             * autoDelete: Should this queue be auto-deleted when its last consumer (if any) unsubscribes?
             * arguments: Optional; additional queue arguments, e.g. "x-queue-type"
             * exclusive:
             *      Should this queue use be limited to its declaring connection? Such a queue will
             *      be deleted when its declaring connection closes.
             * */
            var model = CreateConnection(definition.Connection);
            model.ExchangeDeclare(definition.Connection.Exchange, ExchangeType.Topic, durable: true, autoDelete: false);
            model.QueueDeclare(definition.Name, durable: true, exclusive: false, autoDelete: false, arguments: null);
            model.QueueBind(definition.Name, definition.Connection.Exchange, definition.Route);

            Connections.TryAdd(CombineKey(definition), model);
            return model;
        }
    }
}
