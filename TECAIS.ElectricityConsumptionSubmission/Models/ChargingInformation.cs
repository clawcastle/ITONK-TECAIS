using System;
using System.Collections.Generic;

namespace TECAIS.ElectricityConsumptionSubmission.Models
{
    public class ChargingInformation
    {
        public DateTime Timestamp { get; set; }
        public double CurrentTaxRate { get; set; }
        public IReadOnlyList<double> Charges { get; set; }
        public Guid ConsumerId { get; set; }
    }
}