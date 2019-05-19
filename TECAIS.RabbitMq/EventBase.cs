namespace TECAIS.RabbitMq
{
    public abstract class EventBase
    {
        public string RoutingKey { get; set; }
    }
}