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
        public PricingInformation PricingInformation { get; }
        public ChargingInformation ChargingInformation { get; }
        public DateTime Timestamp { get; }
        private AccountingMessage(string eventType, double amount, PricingInformation pricingInformation, ChargingInformation chargingInformation, DateTime timestamp) : base(eventType)
        {
            Amount = amount;
            PricingInformation = pricingInformation;
            ChargingInformation = chargingInformation;
            Timestamp = timestamp;
        }

        public static AccountingMessage Create(double amount, PricingInformation pricingInformation,
            ChargingInformation chargingInformation)
        {
            return new AccountingMessage("accounting", amount, pricingInformation, chargingInformation, DateTime.Now);
        }
    }
}