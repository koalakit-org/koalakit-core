namespace KoalaKit.Messaging.MessagingBus
{
    //TODO: implement the publisher
    //TODO: enable easy publish (static)
    public interface IPubChannel<TMessage> where TMessage : class, IMessagingBusMessage
    {
    }
}
