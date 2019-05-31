using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingControl.Models
{
    public class AccountingInformation
    {
        public DateTime Timestamp { get; set; }
        public Guid ConsumerId { get; set; }
        public double HeatingCost { get; set; }
        public double WaterCost { get; set; }
        public double ElectricityCost { get; set; }
    }
}
