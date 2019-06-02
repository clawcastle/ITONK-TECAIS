using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.RabbitMq;

namespace TECAIS.AccountingControl.Models.Events
{

    public class AccountingMessage : EventBase
    {
        public double Amount { get; }
        public int HouseID { get; }
        public PricingInformation PricingInformation { get; }
        public ChargingInformation ChargingInformation { get; }
        public DateTime Timestamp { get; }
        public String Type { get; }

        public AccountingMessage(string eventType) : base(eventType)
        {
        }

    }
}