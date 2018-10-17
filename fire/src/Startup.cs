using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Prometheus.Client.Collectors;
using Prometheus.Client.Owin;

namespace Fire
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(new System.Random());
            services.AddMvc();

            CollectorRegistry.Instance.RegisterOnDemandCollectors(new IOnDemandCollector[]{
                new DotNetStatsCollector(),
                new WindowsDotNetStatsCollector()
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UsePrometheusServer()
                .UsePrometheusMiddleware()
                .UseMvc();
        }
    }
}
