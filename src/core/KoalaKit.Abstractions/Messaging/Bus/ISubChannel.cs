namespace KoalaKit.Messaging.Bus
{
    //TODO: enable easy subscribe (static)
    public interface ISubChannel<out TMessage> where TMessage : class, IBusMessage
    {
        Task SubscribeAsync();
        Task SubscribeAsync(Action<TMessage> handler);
        Task UnsubscribeAsync();
    }
}