using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Fire
{
    public static class Program
    {
        public static Task Main(string[] args) => WebHost.CreateDefaultBuilder(args).BuildWebHost().RunAsync();

        public static IWebHost BuildWebHost(this IWebHostBuilder builder) => builder.UseStartup<Startup>().Build();
    }
}
