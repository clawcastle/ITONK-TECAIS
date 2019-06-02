using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.ElectricityConsumptionSubmission.Models.Events;

namespace TECAIS.ElectricityConsumptionSubmission.Models
{
    public class HouseholdModel
    {
        public int ID { get; set; }
        public IEnumerable<AccountingMessage> Invoices { get; set; }
    }
}
