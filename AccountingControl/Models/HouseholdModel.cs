using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TECAIS.AccountingControl.Models
{
    public class HouseholdModel
    {
        public int ID { get; set; }
        public IEnumerable<AccountingInformation> Invoices { get; set; }
    }
}
