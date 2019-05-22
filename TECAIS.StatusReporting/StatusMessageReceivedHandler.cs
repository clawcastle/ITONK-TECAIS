using System;
using System.Threading.Tasks;
using TECAIS.RabbitMq;
using TECAIS.StatusReporting.Models;

namespace TECAIS.StatusReporting
{
    public class StatusMessageReceivedHandler : IEventHandler<StatusReportMessage>
    {
        public Task Handle(StatusReportMessage @event)
        {
            Console.WriteLine($"Received message with status {@event.Status}");
            return Task.CompletedTask;
        }
    }
}