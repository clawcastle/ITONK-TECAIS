using System;

namespace SampleConsumer
{
    public class Measurement
    {
        public Guid Id { get; set; }
        public Guid DeviceId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }
    }
}