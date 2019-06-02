using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TECAIS.AccountingControl.Models
{
    public class AccountingInformation
    {
        public int ID { get; set; }
        public DateTime Timestamp { get; set; }
        public int HouseholdModelID { get; set; }
        public double HeatingCost { get; set; }
        public double WaterCost { get; set; }
        public double ElectricityCost { get; set; }
    }
}
