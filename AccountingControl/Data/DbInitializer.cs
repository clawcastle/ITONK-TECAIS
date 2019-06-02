using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.AccountingControl.Models;

namespace AccountingControl.Data
{
    public class DbInitializer
    {
        public static void InitializeDb(AccountingContext context)
        {
            context.Database.EnsureCreated();

            // Look for any households.
            if (context.Households.Any())
            {
                return;   // DB has been seeded
            }

           

            var households = new HouseholdModel[]
            {
            new HouseholdModel{ ID = 100 },
            new HouseholdModel{ ID = 200 },
            new HouseholdModel{ ID = 300 },
            };
            foreach (HouseholdModel H in households)
            {
                context.Households.Add(H);
            }

            context.Database.OpenConnection();

            try
            {
                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT HouseholdModel ON");
                context.SaveChanges();
                context.Database.ExecuteSqlCommand(@"SET IDENTITY_INSERT HouseholdModel OFF");

            } finally
            {
                context.Database.CloseConnection();
            }
            

            var bills = new AccountingInformation[]
            {
            new AccountingInformation{ HouseholdModelID = 100, BillType = "Heat", Amount = 100, Timestamp = DateTime.Now },
            new AccountingInformation{ HouseholdModelID = 100, BillType = "Water", Amount = 100, Timestamp = DateTime.Now },
            new AccountingInformation{ HouseholdModelID = 100, BillType = "Electricity", Amount = 100, Timestamp = DateTime.Now },
            new AccountingInformation{ HouseholdModelID = 200, BillType = "Heat", Amount = 100, Timestamp = DateTime.Now },
            };
            foreach (AccountingInformation A in bills)
            {
                context.Billings.Add(A);
            }
            context.SaveChanges();
        }
    }
}
