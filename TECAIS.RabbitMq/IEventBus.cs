namespace TECAIS.RabbitMq
{
    public interface IEventBus
    {
        void Deregister();
        void Dispose();
        void Subscribe<TEvent, THandler>(string routingKey = null)
            where TEvent : EventBase
            where THandler : IEventHandler<TEvent>;
    }
}