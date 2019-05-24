using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TECAIS.RabbitMq;
using TECAIS.StatusReporting.Models;
using TECAIS.StatusReporting.Repositories;

namespace TECAIS.StatusReporting.Handlers
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