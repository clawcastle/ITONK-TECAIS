using System;
using TECAIS.RabbitMq;

namespace TECAIS.StatusReporting.Models
{
    public class StatusReportMessage : EventBase
    {
        public int HouseId { get; set; }
        public Guid DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public Status Status { get; set; }
        public double CurrentAmount { get; set; }

        public StatusReportMessage(string eventType) : base(eventType)
        {
        }
    }

    public enum Status
    {
        Ok,
        Warning,
        Error,
        Fatal
    }
}
