using Microsoft.Extensions.Options;

namespace KoalaKit.Messaging
{
    public class DefaultMessageQueuingConnectionSelector<TMessage> : IMessageQueuingConnectionSelector<TMessage> where TMessage : IQueuingMessage, new()
    {
        private const string defaultConnectionName  = "default";
        private readonly MessageQueuingOptions options;

        public DefaultMessageQueuingConnectionSelector(IOptions<MessageQueuingOptions> options)
        {
            this.options = options.Value;

            if (!this.options.Connections.ContainsKey(defaultConnectionName))
                throw new ArgumentException("Message Queuing configuration must contains at least default connection!");
        }

        public MessageQueuingConnectionDefinition Select() => options.Connections[defaultConnectionName];
        public MessageQueuingConnectionDefinition Select(string identifier) => options.Connections[identifier];
    }
}