using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace TECAIS.RabbitMq
{
    public partial class EventHandlerManager : IEventHandlerManager
    {
        private readonly Dictionary<string, List<HandlerInfo>> _eventHandlers;
        private readonly Dictionary<string, Type> _eventTypes;
        public EventHandlerManager()
        {
            _eventHandlers = new Dictionary<string, List<HandlerInfo>>();
            _eventTypes = new Dictionary<string, Type>();
        }

        public void AddEventHandler<TEvent, THandler>(string eventName = null) where TEvent : EventBase where THandler : IEventHandler<TEvent>
        {
            var routingKey = eventName ?? GetRoutingKeyFromEventType<TEvent>();
            AddEventHandlerWithType(typeof(THandler), routingKey);
            _eventTypes.Add(routingKey, typeof(TEvent));
        }

        public IEnumerable<HandlerInfo> GetHandlersForEvent(string eventName)
        {
            var handlers = new List<HandlerInfo>();
            if (!_eventHandlers.ContainsKey(eventName))
            {
                return Enumerable.Empty<HandlerInfo>();
            }

            foreach (var handlerInfo in _eventHandlers[eventName])
            {
                handlers.Add(handlerInfo);
            }

            return handlers;
        }

        public void RemoveHandlerForEvent<TEvent, THandler>()
            where TEvent : EventBase where THandler : IEventHandler<TEvent>
        {
            var routingKey = GetRoutingKeyFromEventType<TEvent>();
            if (!_eventHandlers.ContainsKey(routingKey))
            {
                return;
            }

            var handlerToRemove = _eventHandlers[routingKey]
                .FirstOrDefault(handler => handler.HandlerType == typeof(THandler));
            if (handlerToRemove != null)
            {
                _eventHandlers[routingKey].Remove(handlerToRemove);
            }
        }

        public bool HasHandlersRegisteredForEvent(string routingKey)
        {
            return _eventHandlers.ContainsKey(routingKey);
        }

        public void ClearHandlers()
        {
            _eventHandlers.Clear();
        }
        public string GetRoutingKeyFromEventType<T>()
        {
            return typeof(T).Name;
        }

        public Type GetEventTypeFromRoutingKey(string routingKey)
        {
            return _eventTypes.ContainsKey(routingKey) ? _eventTypes[routingKey] : null;
        }

        private void AddEventHandlerWithType(Type handlerType, string routingKey)
        {
            if (!_eventHandlers.ContainsKey(routingKey))
            {
                _eventHandlers.Add(routingKey, new List<HandlerInfo>());
            }
            if (_eventHandlers[routingKey].Any(h => h.HandlerType == handlerType))
            {
                throw new ArgumentException("A handler of that type has already been subscribed to the event.");
            }
            _eventHandlers[routingKey].Add(new HandlerInfo
            {
                HandlerType = handlerType
            });
        }
    }
}