using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace TECAIS.MeasurementGenerator
{
    public class RabbitMqClient
    {
        private readonly string _exchangeName;
        private readonly ConnectionFactory _connectionFactory;
        public RabbitMqClient(string exchangeName)
        {
            _exchangeName = exchangeName;
            var rabbitHostName = Environment.GetEnvironmentVariable("RABBITMQ_SERVICE_HOST");
            Console.WriteLine($"SERVICE_HOST: {rabbitHostName}");
            _connectionFactory = new ConnectionFactory
                {HostName = rabbitHostName ?? "localhost"};
        }

        public void SendMessage(object messageBody, string routingKey)
        {
            using (var conn = _connectionFactory.CreateConnection())
            {
                using (var channel = conn.CreateModel())
                {
                    channel.ExchangeDeclare(exchange: _exchangeName, type: "direct");
                    var messageBodyJson = JsonConvert.SerializeObject(messageBody);
                    var body = Encoding.UTF8.GetBytes(messageBodyJson);

                    channel.BasicPublish(exchange: _exchangeName, routingKey: routingKey, basicProperties: null,
                        body: body);
                    Console.WriteLine($"Published message: {messageBodyJson} with routing key {routingKey}");
                }
            }
        }
    }
}