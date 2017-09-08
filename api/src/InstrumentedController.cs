using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;
using Prometheus.Client.Collectors;

namespace Fire.Controllers
{

    [Produces("application/json")]
    [Route("")]
    public class InstrumentedController : Controller
    {
        private readonly Counter _requestCounter;
        private readonly Random _random;

        public InstrumentedController(Random random)
        {
            _requestCounter = Metrics.CreateCounter("fire_root_stuff", "Controller instrumented request");
            _random = random;
        }

        [HttpGet("")]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(_random.Next(5, 100));
            _requestCounter.Inc();
            return Ok();
        }
    }
}
