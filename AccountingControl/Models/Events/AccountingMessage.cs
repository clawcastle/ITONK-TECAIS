using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.RabbitMq;

namespace TECAIS.AccountingControl.Models.Events
{

    public class AccountingMessage : EventBase
    {
        public double Amount { get; set; }
        public int HouseID { get; set; }
        public PricingInformation PricingInformation { get; set; }
        public ChargingInformation ChargingInformation { get; set; }
        public DateTime Timestamp { get; set; }
        public String Type { get; set; }

        public AccountingMessage(string eventType) : base(eventType)
        {
        }

    }
}