using System;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using SqlServer;
namespace SetworksSeedData
{
    class Program
    {

        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            SWSeedData.EnsureSeedData(host.Services);
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
