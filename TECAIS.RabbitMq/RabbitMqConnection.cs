using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TECAIS.RabbitMq
{
    public class RabbitMqConnection<T> : IRabbitMqConnection<T>
    {
        private const string ExchangeName = "measurement_exchange";
        private readonly IConnectionFactory _connectionFactory;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _routingKey;

        public RabbitMqConnection(string hostName, string routingKey)
        {
            _connectionFactory = new ConnectionFactory {HostName = hostName};
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();
            _routingKey = routingKey;
        }

        public void Dispose()
        {
            _connection?.Dispose();
            _channel?.Dispose();
        }

        public void RegisterHandler(Action<T> handler)
        {
            _channel.ExchangeDeclare(exchange: ExchangeName, type: "direct");
            var queueName = _channel.QueueDeclare().QueueName;
            _channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: _routingKey);
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var bodyJson = Encoding.UTF8.GetString(body);
                var measurement = JsonConvert.DeserializeObject<T>(bodyJson);
                handler(measurement);
            };
            _channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }

        public void Deregister()
        {
            Dispose();
        }
    }

    public interface IRabbitMqConnection<out T> : IDisposable
    {
        void RegisterHandler(Action<T> handler);
        void Deregister();
    }
}
