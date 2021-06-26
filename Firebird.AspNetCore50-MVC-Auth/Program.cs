using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using NLog.Web;
using System;
using System.IO;

namespace Firebird.AspNetCore50_MVC_Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var iHostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => 
                {
                    webBuilder
                    .UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                     {
                         //logging.ClearProviders();
                     })
                    .UseNLog();  // NLog: setup NLog for Dependency injection

                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var location = new Uri(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        var binDirectory = new FileInfo(location.AbsolutePath).Directory.FullName;
                        AppDomain.CurrentDomain.SetData("BinDirectory", binDirectory);
                        var env = hostingContext.HostingEnvironment;
                        var rootDirectory = env.ContentRootPath;
                        AppDomain.CurrentDomain.SetData("RootDirectory", rootDirectory);
                        string dataDirectory = Path.GetFullPath(Path.Combine(rootDirectory, "App_Data"));
                        if (!Directory.Exists(dataDirectory))
                            Directory.CreateDirectory(dataDirectory);
                        AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);

                        Environment.SetEnvironmentVariable("FIREBIRD_TMP", Path.Combine(dataDirectory, "FIREBIRD_TMP"));
                        Environment.SetEnvironmentVariable("FIREBIRD_LOCK", Path.Combine(dataDirectory, "FIREBIRD_LOCK"));
                    });
                });

            return iHostBuilder;
        }
    }
}
