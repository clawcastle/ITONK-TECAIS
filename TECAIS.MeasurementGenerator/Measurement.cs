using System;

namespace TECAIS.MeasurementGenerator
{
    public class Measurement
    {
        public Guid Id { get; }
        public Guid DeviceId { get; }
        public int HouseId { get; }
        public DateTime Timestamp { get; }
        public DateTime PrevTimestamp { get; }
        public double Value { get; }
        public double PrevValue { get; }
        public MeasurementType Type { get; }

        private Measurement(Guid id, Guid deviceId, int houseId, DateTime timestamp, DateTime ptimestamp, double value, double pvalue, MeasurementType type)
        {
            Id = id;
            DeviceId = deviceId;
            HouseId = houseId;
            Timestamp = timestamp;
            PrevTimestamp = ptimestamp;
            Value = value;
            PrevValue = pvalue;
            Type = type;
        }

        public static Measurement Create(Guid deviceId, int houseId, DateTime ptime, double value, double pvalue, MeasurementType type)
        {
            return new Measurement(Guid.NewGuid(), deviceId, houseId, DateTime.Now, ptime, value, pvalue, type);
        }
    }

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