namespace KoalaKit.Messaging.MessagingBus
{

    //TODO: implement the subscriber
    //TODO: enable easy subscribe (static)
    public interface ISubChannel<TMessage> where TMessage : class, IMessagingBusMessage
    {
    }
}