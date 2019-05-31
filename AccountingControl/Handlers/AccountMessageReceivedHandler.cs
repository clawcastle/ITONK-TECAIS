using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.RabbitMq;
using TECAIS.AccountingControl.Models.Events;

namespace AccountingControl.Handlers
{
    public class AccountMessageReceivedHandler : IEventHandler<AccountingMessage>
    {
        public Task Handle(AccountingMessage @event)
        {
            Console.WriteLine($"Received message with status {@event.Amount}");
            return Task.CompletedTask;
        }
    }
}
