namespace KoalaKit.Messaging
{
    public class MessageQueuingOptions
    {
        public Dictionary<string, MessageQueuingConnectionDefinition> Connections { get; set; } = new Dictionary<string, MessageQueuingConnectionDefinition>();
    }
}
