namespace KoalaKit.Messaging.Bus
{
    //channel
    public interface IBusMessageDefinitionFactory<in TMessage> 
        where TMessage : IBusMessage, new()
    {
        BusMessageDefinition Create();
        BusMessageDefinition Create(TMessage message);
    }
}
