using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace Firebird.AspNetCore22_MVC_Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            IWebHostBuilder webhostbuilder = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    //logging.ClearProviders();
                })
                .UseNLog();  // NLog: setup NLog for Dependency injection



            webhostbuilder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                var rootDirectory = env.ContentRootPath;
                AppDomain.CurrentDomain.SetData("RootDirectory", rootDirectory);
                string absolute = Path.GetFullPath(Path.Combine(rootDirectory, "App_Data"));
                if (!Directory.Exists(absolute))
                    Directory.CreateDirectory(absolute);
                AppDomain.CurrentDomain.SetData("DataDirectory", absolute);
            });
            return webhostbuilder;
        }
    }
}
