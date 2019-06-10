using System;

namespace TECAIS.MeasurementGenerator
{
    public class MeasurementDevice
    {
        public Guid Id { get; }
        public int HouseId { get; }
        public MeasurementType MeasurementType { get; }
        public DateTime PreviousTime { get; private set; }
        public string SerialNumber { get; }
        public string Manufacturer { get; }
        public double CurrentValue { get; private set; }
        public double PreviousValue { get; private set; }

        public MeasurementDevice(Guid id, int houseId, MeasurementType measurementType, string serialNumber, string manufacturer, double startingValue)
        {
            Id = id;
            HouseId = houseId;
            MeasurementType = measurementType;
            SerialNumber = serialNumber ?? throw new ArgumentNullException(nameof(serialNumber));
            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
            CurrentValue = startingValue;
            PreviousValue = 0;
            PreviousTime = DateTime.Now;
        }

        public Measurement GenerateMeasurement()
        {
            UpdateValue();
            Measurement m = Measurement.Create(Id, HouseId != 0 ? HouseId : new Random().Next(0, 10), CurrentValue, MeasurementType);
            PreviousTime = DateTime.Now;
            return m;
        }

        public StatusReportMessage GenerateStatusReport(int houseId)
        {
            return new StatusReportMessage
            {
                DeviceId = Id,
                Message = "All is good",
                Status = Status.Ok,
                Timestamp = DateTime.Now
            };
        }

        private void UpdateValue()
        {
            var random = new Random();
            CurrentValue += random.NextDouble() * 100;
            PreviousValue = CurrentValue;
        }
    }
}