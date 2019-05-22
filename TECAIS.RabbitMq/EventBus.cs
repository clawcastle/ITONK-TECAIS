using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TECAIS.RabbitMq
{
    public class EventBus : IEventBus
    {
        private const string ExchangeName = "measurement_exchange";
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private readonly IEventHandlerManager _eventHandlerManager;
        private readonly IServiceProvider _serviceProvider;
        private string _queueName;

        public EventBus(IEventHandlerManager eventHandlerManager, IServiceProvider serviceProvider, string hostName = "localhost")
        {
            _connectionFactory = new ConnectionFactory {HostName = hostName};
            _connection = _connectionFactory.CreateConnection();
            _channel = CreateChannel();
            _eventHandlerManager = eventHandlerManager;
            _serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }

        public void Publish(EventBase @event)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: ExchangeName, type: "direct");
                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.DeliveryMode = 2; //persistent

                channel.BasicPublish(exchange: ExchangeName, routingKey: @event.EventType, basicProperties: properties,
                    body: body);
            }
        }
        public void Subscribe<TEvent, THandler>(string eventName = null) where TEvent : EventBase where THandler : IEventHandler<TEvent>
        {
            var routingKey = eventName ?? _eventHandlerManager.GetRoutingKeyFromEventType<TEvent>();
            if (!_eventHandlerManager.HasHandlersRegisteredForEvent(routingKey))
            {
                TryConnect();
                using (var channel = _connection.CreateModel())
                {
                    channel.QueueBind(queue: _queueName, exchange: ExchangeName, routingKey: routingKey);
                }
            }
            _eventHandlerManager.AddEventHandler<TEvent, THandler>(routingKey);
        }

        private IModel CreateChannel()
        {
            TryConnect();
            var channel = _connection.CreateModel();

            channel.ExchangeDeclare(exchange: ExchangeName, type: "direct", durable: false, autoDelete: false,
                arguments: null);
            _queueName = channel.QueueDeclare().QueueName;
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body);

                await ProcessEvent(eventName, message);
                channel.BasicAck(ea.DeliveryTag, multiple: false);
            };

            channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
            channel.CallbackException += (sender, ea) =>
            {
                _channel.Dispose();
                _channel = CreateChannel();
            };

            return channel;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_eventHandlerManager.HasHandlersRegisteredForEvent(eventName))
            {
                var eventType = _eventHandlerManager.GetEventTypeFromRoutingKey(eventName);
                var eventDeserialized = JsonConvert.DeserializeObject(message, eventType);
                var handlers = _eventHandlerManager.GetHandlersForEvent(eventName);

                foreach (var handlerInfo in handlers)
                {
                    var handler = _serviceProvider.GetService(handlerInfo.HandlerType);
                    if (handler == null)
                    {
                        continue;
                    }

                    var concreteHandler = typeof(IEventHandler<>).MakeGenericType(eventType);
                    await (Task) concreteHandler.GetMethod("Handle").Invoke(handler, new object[] {eventDeserialized});
                }
            }
        }

        public void Deregister()
        {
            Dispose();
        }

        private void TryConnect()
        {
            var isConnected = _connection != null && _connection.IsOpen;
            if (!isConnected)
            {
                _connection = _connectionFactory.CreateConnection();
            }
        }
    }
}
