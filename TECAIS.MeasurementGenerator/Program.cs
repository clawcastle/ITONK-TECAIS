using System;
using System.Collections.Generic;
using System.Threading;

namespace TECAIS.MeasurementGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var rabbitMqClient = new RabbitMqClient(exchangeName: "measurement_exchange");
            var houseHolds = new List<HouseHold>
            {
                new HouseHold(1, new List<MeasurementDevice>
                {
                    new MeasurementDevice(Guid.NewGuid(), 1, MeasurementType.Electricity, "123456789", "Mig", 123),
                    new MeasurementDevice(Guid.NewGuid(), 1, MeasurementType.Heat, "123456789", "Mig", 456),
                    new MeasurementDevice(Guid.NewGuid(), 1, MeasurementType.Water, "123456789", "Mig", 789),
                }),
                new HouseHold(2, new List<MeasurementDevice>
                {
                    new MeasurementDevice(Guid.NewGuid(), 2, MeasurementType.Electricity, "123456789", "Mig", 123),
                    new MeasurementDevice(Guid.NewGuid(), 2, MeasurementType.Heat, "123456789", "Mig", 456),
                    new MeasurementDevice(Guid.NewGuid(), 2, MeasurementType.Water, "123456789", "Mig", 789),
                })
            };
            
            while (true)
            {
                ReportMeasurements(houseHolds, rabbitMqClient);
                ReportStatus(houseHolds, rabbitMqClient);
                Thread.Sleep(5000);
            }
        }

        private static void ReportMeasurements(IEnumerable<HouseHold> houseHolds, RabbitMqClient rabbitMqClient)
        {
            foreach (var houseHold in houseHolds)
            {
                foreach (var measurementDevice in houseHold.MeasurementDevices)
                {
                    var measurement = measurementDevice.GenerateMeasurement();
                    rabbitMqClient.SendMessage(messageBody: measurement, routingKey: measurement.Type.ToString().ToLower());
                }
            }
        }

        private static void ReportStatus(IEnumerable<HouseHold> houseHolds, RabbitMqClient rabbitMqClient)
        {
            foreach (var houseHold in houseHolds)
            {
                foreach (var measurementDevice in houseHold.MeasurementDevices)
                {
                    var status = measurementDevice.GenerateStatusReport();
                    rabbitMqClient.SendMessage(messageBody: status, routingKey: "status_report");
                }
            }
        }
    }
}
