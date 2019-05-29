using System;
using System.Linq;
using System.Threading.Tasks;

namespace TECAIS.ElectricityConsumptionSubmission.Models
{
    public class PricingInformation
    {
        public DateTime Timestamp { get; set; }
        public long MillisUTC { get; set; }
        public double Price { get; set; }
    }
}
