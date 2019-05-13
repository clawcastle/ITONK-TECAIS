using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleConsumer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMeasurements()
        {
            return Ok(Measurements.MeasurementsList);
        }
    }
}