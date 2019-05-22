using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.RabbitMq;

namespace TECAIS.StatusReporting.Models
{
    public class StatusReportMessage : EventBase
    {
        public Guid DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public Status Status { get; set; }
        public string Message { get; set; }

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
