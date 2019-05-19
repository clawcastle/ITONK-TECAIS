using System;
using TECAIS.RabbitMq;

namespace TECAIS.ElectricityConsumptionSubmission
{
    public class Measurement : EventBase
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
    }
}