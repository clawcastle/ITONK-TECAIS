using System;
using System.Linq;
using System.Threading.Tasks;

namespace TECAIS.AccountingControl.Models
{
    public class PricingInformation
    {
        public DateTime Timestamp { get; set; }
        public long MillisUTC { get; set; }
        public double Price { get; set; }
    }
}
