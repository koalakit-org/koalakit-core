using KoalaKit.Serializations;

namespace KoalaKit.Messaging
{
    public class MessagingSerializer<TMessage> : KoalaJsonSerializer<TMessage> where TMessage : IKoalaMessage, new()
    {
        public override TMessage? Deserialize(byte[] bytes) => base.Deserialize(bytes);
        public override byte[] Serialize(TMessage message) => base.Serialize(message);
    }
}
