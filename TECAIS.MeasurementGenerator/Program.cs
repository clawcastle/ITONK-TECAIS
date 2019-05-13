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
                new HouseHold(new List<MeasurementDevice>
                {
                    new MeasurementDevice(Guid.NewGuid(), MeasurementType.Electricity, "123456789", "Mig", 123),
                    new MeasurementDevice(Guid.NewGuid(), MeasurementType.Heat, "123456789", "Mig", 456),
                    new MeasurementDevice(Guid.NewGuid(), MeasurementType.Water, "123456789", "Mig", 789),
                })
            };
            while (true)
            {
                ReportMeasurements(houseHolds, rabbitMqClient);
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
    }
}
