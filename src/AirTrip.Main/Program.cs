using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AirTrip.Main
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddCommandLine(args)
                .Build();

            new WebHostBuilder()
                .UseUrls("http://*:5000")
                .UseConfiguration(config)
                .ConfigureLogging(i => { i.AddConsole(); })
                .UseKestrel()
                .UseStartup<Startup>()
                .Build()
                .Run();
        }
    }    
}