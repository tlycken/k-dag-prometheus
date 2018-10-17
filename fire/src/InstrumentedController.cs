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
            await Task.Delay(GetDelay());
            _requestCounter.Inc();
            Console.WriteLine("Incremented fire_root_stuff");
            return Ok();
        }

        [HttpGet("foo"), HttpPost("foo")]
        public async Task<IActionResult> FooAsync()
        {
            await Task.Delay(GetDelay());
            return Ok();
        }

        private int GetDelay()
        {
            var u1 = 1.0 - _random.NextDouble(); //uniform(0,1] random doubles
            var u2 = 1.0 - _random.NextDouble();
            var randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            var randNormal = 500 + 250 * randStdNormal;

            var delay = Math.Max((int)randNormal, 10);

            return delay;
        }
    }
}
