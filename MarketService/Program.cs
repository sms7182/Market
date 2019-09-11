using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MarketService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            //WebHost.CreateDefaultBuilder(args)
            //.UseKestrel().UseUrls("http://*:5000")
            //.UseContentRoot(Directory.GetCurrentDirectory()).UseIISIntegration().UseStartup<Startup>().B
            WebHost.CreateDefaultBuilder(args).UseUrls("https://*:5001")
                .UseStartup<Startup>();
    }
}
