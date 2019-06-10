using System;

namespace TECAIS.MeasurementGenerator
{
    public class Measurement
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public int HouseId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
        public MeasurementType Type { get; set; }

        private Measurement(Guid id, Guid deviceId, int houseId, DateTime timestamp, double value, MeasurementType type)
        {
            Id = id;
            DeviceId = deviceId;
            HouseId = houseId;
            Timestamp = timestamp;
            Value = value;
            Type = type;
        }

        public static Measurement Create(Guid deviceId, int houseId, double value, MeasurementType type)
        {
            return new Measurement(Guid.NewGuid(), deviceId, houseId, DateTime.Now, value, type);
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