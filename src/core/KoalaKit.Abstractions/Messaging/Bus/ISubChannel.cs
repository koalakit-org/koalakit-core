namespace KoalaKit.Messaging.Bus
{
    //TODO: enable easy subscribe (static)
    public interface ISubChannel<out TMessage>
        where TMessage : IBusMessage, new()
    {
        Task SubscribeAsync();
        Task SubscribeAsync(Action<TMessage> handler);
        Task UnsubscribeAsync();
    }
}