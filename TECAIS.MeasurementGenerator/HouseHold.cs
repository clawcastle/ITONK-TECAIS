using System.Collections.Generic;
using System.Linq;

namespace TECAIS.MeasurementGenerator
{
    public class HouseHold
    {
        public IReadOnlyList<MeasurementDevice> MeasurementDevices { get; }

        public HouseHold(IEnumerable<MeasurementDevice> measurementDevices)
        {
            MeasurementDevices = measurementDevices.ToList();
        }
    }
}