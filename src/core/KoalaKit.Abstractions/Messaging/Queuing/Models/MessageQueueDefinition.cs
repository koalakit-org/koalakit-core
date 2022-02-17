namespace KoalaKit.Messaging.Queuing
{
    public class MessageQueueDefinition
    {
        public MessageQueueDefinition(string name, string route, MessageQueuingConnectionDefinition connection)
        {
            this.Name = name;
            this.Route = route;
            this.Connection = connection; 
        }

        public string Name { get; set; }
        public string Route { get; set; }
        public MessageQueuingConnectionDefinition Connection { get; set; }
    }
}
