using Firebird.AspNetCore50_MVC_Auth.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Firebird.AspNetCore50_MVC_Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var binDirectory = AppDomain.CurrentDomain.GetData("BinDirectory").ToString();
            var dataDirectory = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var fbserverDirectory = "fbs-x64";
#if x86
            fbserverDirectory = "fbs-x86";
#endif
            var connection = new FbConnectionStringBuilder(Configuration.GetConnectionString("DefaultConnection"));
            connection.ClientLibrary = Path.Combine(Path.Combine(binDirectory, fbserverDirectory), Path.GetFileName(connection.ClientLibrary));
            connection.Database = Path.Combine(dataDirectory,Path.GetFileName(connection.Database));
            var connectionstring = connection.ToString();
			
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseFirebird(connectionstring
                ));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
