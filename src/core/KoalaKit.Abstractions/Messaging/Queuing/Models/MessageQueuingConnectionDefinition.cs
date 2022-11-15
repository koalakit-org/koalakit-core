namespace KoalaKit.Messaging.Queuing
{
    public class MessageQueuingConnectionDefinition
    {
        public string Host { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 5672;
        public string VirtualHost { get; set; } = "/";
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string Exchange { get; set; } = string.Empty;
    }
}
