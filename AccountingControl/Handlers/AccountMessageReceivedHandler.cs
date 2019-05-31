using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.RabbitMq;
using TECAIS.AccountingControl.Models.Events;
using AccountingControl.Models;

namespace AccountingControl.Handlers
{
    public class AccountMessageReceivedHandler : IEventHandler<AccountingMessage>
    {
        public Task Handle(AccountingMessage @event)
        {
            Console.WriteLine($"Received message with status {@event.Amount}");
            var AccInfo = new AccountingInformation();
            AccInfo.ConsumerId = @event.ChargingInformation.ConsumerId;
            AccInfo.ElectricityCost = @event.PricingInformation.Price;
            return Task.CompletedTask;
        }
    }
}
