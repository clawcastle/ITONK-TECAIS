using System;
using System.Collections.Generic;

namespace TECAIS.RabbitMq
{
    public interface IEventHandlerManager
    {
        void AddEventHandler<TEvent, THandler>(string eventName = null)
            where TEvent : EventBase
            where THandler : IEventHandler<TEvent>;
        void ClearHandlers();
        IEnumerable<HandlerInfo> GetHandlersForEvent(string eventName);
        void RemoveHandlerForEvent<TEvent, THandler>()
            where TEvent : EventBase
            where THandler : IEventHandler<TEvent>;

        string GetRoutingKeyFromEventType<T>();
        bool HasHandlersRegisteredForEvent(string routingKey);
        Type GetEventTypeFromRoutingKey(string routingKey);
    }
}