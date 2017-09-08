using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Prometheus.Client;

namespace Fire
{
    public class PrometheusMiddleware
    {
        private readonly Func<HttpContext, Task> _next;
        private readonly Counter _counter;
        private readonly Histogram _histogram;

        public PrometheusMiddleware(Func<HttpContext, Task> next)
        {
            _next = next;
            _counter = Metrics.CreateCounter("http_requests_total", "Total HTTP requests to the Fire API", new[] { "path", "method" });
            _histogram = Metrics.CreateHistogram("http_request_duration_microseconds", "");
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;
            var method = context.Request.Method;

            _counter.Labels(path, method).Inc();

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            await _next?.Invoke(context);
            var elapsed = stopwatch.Elapsed.TotalMilliseconds * 1000;
            _histogram.Observe(elapsed);
        }
    }
}
