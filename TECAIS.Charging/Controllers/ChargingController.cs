using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace TECAIS.Charging.Controllers
{
    [ApiController]
    public class ChargingController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetChargingInformation()
        {
            var chargingInformation = GenerateChargingInformation();
            return Ok(chargingInformation);
        }

        private ChargingInformation GenerateChargingInformation()
        {
            var charges = new List<double>();
            var rng = new Random();
            var currentTaxRate = 2 + rng.NextDouble();
            for (int i = 0; i < 3; i++)
            {
                var charge = 5 * rng.NextDouble();
                charges.Add(charge);
            }

            var chargingInformation = new ChargingInformation
            {
                Charges = charges,
                CurrentTaxRate = currentTaxRate,
                Timestamp = DateTime.Now
            };
            return chargingInformation;
        }

        public class ChargingInformation
        {
            public DateTime Timestamp { get; set; }
            public double CurrentTaxRate { get; set; }
            public IReadOnlyList<double> Charges { get; set; } = new List<double>();
        }
    }
}
