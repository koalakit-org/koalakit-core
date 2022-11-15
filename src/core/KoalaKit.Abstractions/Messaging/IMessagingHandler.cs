namespace KoalaKit.Messaging
{
    public interface IMessagingHandler<in TMessage> 
        where TMessage : IKoalaMessage, new()
    {
        Task<bool> HandleAsync(TMessage message);
    }
}
