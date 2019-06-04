using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.RabbitMq;
using TECAIS.AccountingControl.Models.Events;
using TECAIS.AccountingControl.Models;
using AccountingControl.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountingControl.Handlers
{
    public class AccountMessageReceivedHandler : IEventHandler<AccountingMessage>
    {

        private readonly AccountingContext _context;

        public AccountMessageReceivedHandler(AccountingContext context)
        {
            _context = context;
        }

        public Task Handle(AccountingMessage @event)
        {
            if(@event.HouseID != 0)
            {

            HouseholdModel Home = new HouseholdModel { ID = @event.HouseID };

            List<HouseholdModel> listHouse = _context.Households.ToList();

            bool found = false;

            foreach (HouseholdModel H in listHouse)
            {
                if (H.ID == @event.HouseID) { found = true; }
            }

            Console.WriteLine($"Received message with amount {@event.Amount}");
            if (!found)
            {
                _context.Households.Add(Home);

                _context.Database.OpenConnection();

                try
                {
                    _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT HouseholdModel ON");
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT HouseholdModel OFF");

                }
                finally
                {
                    _context.Database.CloseConnection();
                }
            }
            
            var AccInfo = new AccountingInformation{ HouseholdModelID = @event.HouseID, BillType = @event.Type, Amount = @event.Amount, Timestamp = @event.Timestamp};

            _context.Billings.Add(AccInfo);

            _context.SaveChanges();
            }
            return Task.CompletedTask;
            
        }
    }
}
