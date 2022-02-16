namespace KoalaKit.Messaging.Bus
{
    //TODO: enable easy subscribe (static)
    public interface ISubChannel<TMessage> where TMessage : class, IBusMessage
    {
        Task SubscribeAsync();
        Task UnsubscribeAsync();
    }
}