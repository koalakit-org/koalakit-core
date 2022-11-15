using Microsoft.Extensions.Options;

namespace KoalaKit.Messaging.Queuing
{
    public class DefaultMessageQueuingConnectionSelector<TMessage> : IMessageQueuingConnectionSelector<TMessage> where TMessage : IQueuingMessage, new()
    {
        private const string defaultConnectionName = "default";
        private readonly MessageQueuingOptions options;

        public DefaultMessageQueuingConnectionSelector(IOptions<MessageQueuingOptions> options)
        {
            this.options = options.Value;

            if (!this.options.Connections.ContainsKey(defaultConnectionName))
                throw new ArgumentException("Message Queuing configuration must contains at least default connection!");
        }

        public MessageQueuingConnectionDefinition Select() => options.Connections[defaultConnectionName];
        public MessageQueuingConnectionDefinition Select(string identifier)
        {
            if (options.Connections.ContainsKey(identifier))
                return options.Connections[identifier];

            return Select();

        }
    }
}