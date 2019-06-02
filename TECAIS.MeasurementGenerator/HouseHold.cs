using System.Collections.Generic;
using System.Linq;

namespace TECAIS.MeasurementGenerator
{
    public class HouseHold
    {
        public int ID { get; set; }
        public IReadOnlyList<MeasurementDevice> MeasurementDevices { get; }

        public HouseHold(int id, IEnumerable<MeasurementDevice> measurementDevices)
        {
            ID = id;
            MeasurementDevices = measurementDevices.ToList();
        }
    }
}