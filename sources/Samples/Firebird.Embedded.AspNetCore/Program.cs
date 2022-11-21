using Firebird.Embedded.AspNetCore.Data;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.WindowsServices;

namespace Firebird.Embedded.AspNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                Args = args,
                ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
            });

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            #region MyRegion

            var binDirectory=AppDomain.CurrentDomain.BaseDirectory;
            var env = builder.Environment;
            var rootDirectory = env.ContentRootPath;
            var dataDirectory = Path.GetFullPath(Path.Combine(rootDirectory, "App_Data"));
            if (!Directory.Exists(dataDirectory))
                Directory.CreateDirectory(dataDirectory);

            //var @lock=Environment.GetEnvironmentVariable("FIREBIRD_LOCK");
            //var tmp = Environment.GetEnvironmentVariable("FIREBIRD_TMP");

            //Environment.SetEnvironmentVariable("FIREBIRD_TMP", Path.Combine(dataDirectory, "FIREBIRD_TMP"));
            //Environment.SetEnvironmentVariable("FIREBIRD_LOCK", Path.Combine(dataDirectory, "FIREBIRD_LOCK"));

#if AnyCPU
            var fbserverDirectory = "FES-x64";
#endif
#if x86
            var fbserverDirectory = "FES-x86";
#endif
#if x64
            var fbserverDirectory = "FES-x64";
#endif
            var connection = new FbConnectionStringBuilder(connectionString);
            connection.ClientLibrary = Path.Combine(Path.Combine(binDirectory, fbserverDirectory), Path.GetFileName(connection.ClientLibrary));
            connection.Database = Path.Combine(dataDirectory, Path.GetFileName(connection.Database));
            connection.ServerType=FbServerType.Embedded;
            connectionString = connection.ToString();
            if (!File.Exists(connection.Database))
            {
                FbConnection.CreateDatabase(connectionString, pageSize: 16384);
            }
            #endregion

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseFirebird(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}