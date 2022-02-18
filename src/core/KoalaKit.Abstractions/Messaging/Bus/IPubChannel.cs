namespace KoalaKit.Messaging.Bus
{
    //TODO: enable easy publish (static)
    public interface IPubChannel<in TMessage>
        where TMessage : IBusMessage, new()
    {
        Task PublishAsync(TMessage message);
    }
}
