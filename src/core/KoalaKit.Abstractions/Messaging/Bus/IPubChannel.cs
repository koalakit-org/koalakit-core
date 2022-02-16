namespace KoalaKit.Messaging.Bus
{
    //TODO: enable easy publish (static)
    public interface IPubChannel<in TMessage> where TMessage : class, IBusMessage
    {
        Task PublishAsync(TMessage message);
    }
}
