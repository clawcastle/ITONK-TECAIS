using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.RabbitMq;
using TECAIS.AccountingControl.Models.Events;
using TECAIS.AccountingControl.Models;
using AccountingControl.Data;
using Microsoft.EntityFrameworkCore;
using log4net;

namespace AccountingControl.Handlers
{
    public class AccountMessageReceivedHandler : IEventHandler<AccountingMessage>
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly AccountingContext _context;

        public AccountMessageReceivedHandler(AccountingContext context)
        {
            _context = context;
        }

        public Task Handle(AccountingMessage @event)
        {
            _log.Debug($"Entering Handler with event - Id: {@event.HouseId}, Amount {@event.Amount}, Timestamp {@event.Timestamp}, Type: {@event.Type}");
            try
            {
                if (@event.HouseId != 0)
                {
                    _log.Debug("Entering if-statement(@event.HouseId): " + @event.HouseId);
                    HouseholdModel Home = new HouseholdModel { ID = @event.HouseId };

                    List<HouseholdModel> listHouse = _context.Households.ToList();

                    bool found = false;

                    foreach (HouseholdModel H in listHouse)
                    {
                        if (H.ID == @event.HouseId) { found = true; }
                    }

                    Console.WriteLine($"Received message with amount {@event.Amount}");
                    if (!found)
                    {
                        _log.Debug("Entering Not-Found statement");
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


                    var AccInfo = new AccountingInformation { HouseholdModelID = @event.HouseId, BillType = @event.Type, Amount = @event.Amount, Timestamp = @event.Timestamp };

                    _context.Billings.Add(AccInfo);

                    _context.SaveChanges();
                }
                _log.Debug("Returning from Handler");
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                _log.Error("Handle in AccountMessageReceivedHandler failed with exception: " + ex);
                throw;
            }
            
            
        }
    }
}
