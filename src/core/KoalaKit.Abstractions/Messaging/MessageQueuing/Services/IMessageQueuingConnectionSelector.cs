namespace KoalaKit.Messaging
{
    public interface IMessageQueuingConnectionSelector<TMessage> where TMessage : IQueuingMessage, new()
    {
        MessageQueuingConnectionDefinition Select();
        MessageQueuingConnectionDefinition Select(string identifier);
    }
}
