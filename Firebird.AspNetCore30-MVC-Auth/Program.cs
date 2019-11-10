using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Firebird.AspNetCore30_MVC_Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                    {
                    })
                    .UseNLog();  // NLog: setup NLog for Dependency injection
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var env = hostingContext.HostingEnvironment;
                        var rootDirectory = env.ContentRootPath;
                        AppDomain.CurrentDomain.SetData("RootDirectory", rootDirectory);
                        string absolute = Path.GetFullPath(Path.Combine(rootDirectory, "App_Data"));
                        if (!Directory.Exists(absolute))
                            Directory.CreateDirectory(absolute);
                        AppDomain.CurrentDomain.SetData("DataDirectory", absolute);
                    });
                });
    }
}
