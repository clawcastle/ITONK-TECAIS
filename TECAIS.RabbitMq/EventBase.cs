namespace TECAIS.RabbitMq
{
    public abstract class EventBase
    {
        public string EventType { get; }

        public EventBase(string eventType)
        {
            EventType = eventType;
        }
    }
}