using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TECAIS.StatusReporting.Models
{
    public class StatusReportMessage
    {
        public Guid DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public Status Status { get; set; }
        public string Message { get; set; }
    }

    public enum Status
    {
        Ok,
        Warning,
        Error,
        Fatal
    }
}
