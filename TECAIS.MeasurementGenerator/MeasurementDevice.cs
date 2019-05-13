using System;

namespace TECAIS.MeasurementGenerator
{
    public class MeasurementDevice
    {
        public Guid Id { get; }
        public MeasurementType MeasurementType { get; }
        public string SerialNumber { get; }
        public string Manufacturer { get; }
        public double CurrentValue { get; private set; }

        public MeasurementDevice(Guid id, MeasurementType measurementType, string serialNumber, string manufacturer, double startingValue)
        {
            Id = id;
            MeasurementType = measurementType;
            SerialNumber = serialNumber ?? throw new ArgumentNullException(nameof(serialNumber));
            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
            CurrentValue = startingValue;
        }

        public Measurement GenerateMeasurement()
        {
            UpdateValue();
            return Measurement.Create(Id, CurrentValue, MeasurementType);
        }

        private void UpdateValue()
        {
            var random = new Random();
            CurrentValue += random.NextDouble() * 100;
        }
    }
}