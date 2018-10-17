using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Prometheus.Client;

namespace Fire
{
    public class PrometheusMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Counter _counter;
        private readonly Summary _requestDuration;

        public PrometheusMiddleware(RequestDelegate next)
        {
            _next = next;
            _counter = Metrics.CreateCounter("http_requests_total", "Total HTTP requests to the Fire API", new[] { "path", "method" });
            _requestDuration = Metrics.CreateSummary("http_request_duration_microseconds", "", new[]{"path", "method"});
            Console.WriteLine("Instantiated metrics");
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path;
            var method = context.Request.Method;

            _counter.Labels(path, method).Inc();

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            await _next?.Invoke(context);
            var elapsed = stopwatch.Elapsed.TotalMilliseconds * 1000;
            _requestDuration.Labels(path, method).Observe(elapsed);
        }
    }

    public static class PrometheusMiddlewareExtensions
    {
        public static IApplicationBuilder UsePrometheusMiddleware(this IApplicationBuilder app) => app.UseMiddleware<PrometheusMiddleware>();
    }
}
