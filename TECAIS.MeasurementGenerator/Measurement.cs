using System;

namespace TECAIS.MeasurementGenerator
{
    public class Measurement
    {
        public Guid Id { get; }
        public Guid DeviceId { get; }
        public DateTime Timestamp { get; }
        public double Value { get; }
        public MeasurementType Type { get; }

        private Measurement(Guid id, Guid deviceId, DateTime timestamp, double value, MeasurementType type)
        {
            Id = id;
            DeviceId = deviceId;
            Timestamp = timestamp;
            Value = value;
            Type = type;
        }

        public static Measurement Create(Guid deviceId, double value, MeasurementType type)
        {
            return new Measurement(Guid.NewGuid(), deviceId, DateTime.Now, value, type);
        }
    }
}