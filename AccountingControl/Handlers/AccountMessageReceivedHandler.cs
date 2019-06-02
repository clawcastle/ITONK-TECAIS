using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.RabbitMq;
using TECAIS.AccountingControl.Models.Events;
using TECAIS.AccountingControl.Models;

namespace AccountingControl.Handlers
{
    public class AccountMessageReceivedHandler : IEventHandler<AccountingMessage>
    {
        public Task Handle(AccountingMessage @event)
        {
            Console.WriteLine($"Received message with amount {@event.Amount}");



            var AccInfo = new AccountingInformation{ HouseholdModelID = @event.HouseID, BillType = @event.Type, Amount = @event.Amount, Timestamp = @event.Timestamp};
           
            return Task.CompletedTask;
        }
    }
}
