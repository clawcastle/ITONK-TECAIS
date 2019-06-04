using System;
using System.Collections.Generic;
using System.Linq;

namespace TECAIS.MeasurementGenerator
{
    public class HouseHold
    {
        public int Id { get; set; }
        public IReadOnlyList<MeasurementDevice> MeasurementDevices { get; }

        public HouseHold(int id, IEnumerable<MeasurementDevice> measurementDevices)
        {
            Id = id;
            MeasurementDevices = measurementDevices.ToList();
        }
    }
}