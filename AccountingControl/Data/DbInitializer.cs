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

            // Look for any students.
            if (context.Households.Any())
            {
                return;   // DB has been seeded
            }

            var households = new HouseholdModel[]
            {
            new HouseholdModel{},
            new HouseholdModel{},
            new HouseholdModel{},
            };
            foreach (HouseholdModel H in households)
            {
                context.Households.Add(H);
            }
            context.SaveChanges();

            var bills = new AccountingInformation[]
            {
            new AccountingInformation{ ElectricityCost = 200, HeatingCost = 200, HouseholdModelID = 1, Timestamp = DateTime.Now },
            new AccountingInformation{ ElectricityCost = 100, HeatingCost = 100, HouseholdModelID = 1, Timestamp = DateTime.Now },
            new AccountingInformation{ ElectricityCost = 200, HeatingCost = 200, HouseholdModelID = 2, Timestamp = DateTime.Now },
            new AccountingInformation{ ElectricityCost = 2300, HeatingCost = 2300, HouseholdModelID = 3, Timestamp = DateTime.Now },
            };
            foreach (AccountingInformation A in bills)
            {
                context.Billings.Add(A);
            }
            context.SaveChanges();
        }
    }
}
