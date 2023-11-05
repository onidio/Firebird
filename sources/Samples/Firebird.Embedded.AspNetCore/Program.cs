using Firebird.Embedded.AspNetCore.Data;
using Firebird.Embedded.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting.WindowsServices;

namespace Firebird.Embedded.AspNetCore
{
    

    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(new WebApplicationOptions
            {
                Args = args,
                ContentRootPath = WindowsServiceHelpers.IsWindowsService() ? AppContext.BaseDirectory : default
            });

            // Add services to the container.
            //var connectionStr = builder.Configuration.GetConnectionString("DefaultConnection") ??
            //                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            //#region MyRegion

            ////var @lock=Environment.GetEnvironmentVariable("FIREBIRD_LOCK");
            ////var tmp = Environment.GetEnvironmentVariable("FIREBIRD_TMP");

            ////Environment.SetEnvironmentVariable("FIREBIRD_TMP", Path.Combine(dataDirectory, "FIREBIRD_TMP"));
            ////Environment.SetEnvironmentVariable("FIREBIRD_LOCK", Path.Combine(dataDirectory, "FIREBIRD_LOCK"));


            //var connection = new FbConnectionStringBuilder(connectionString);
            //connectionString = connection.ToString();
            ////if (!File.Exists(connection.Database))
            ////{
            ////    FbConnection.CreateDatabase(connectionString, pageSize: 16384);
            ////}
            //#endregion

            builder.Services.AddDbContext<ApplicationDbContext>(
                options => options.UseFirebirdEmbedded(
                    embeddedOptionsAction: opt =>
                    {
                        opt.DatabaseName = "IDENTITY.FDB";
                        opt.DatabaseDirectory = new EmbeddedDatabaseDirectory(Path.Combine(builder.Environment.ContentRootPath, "App_Data"));
                    }
                    //,connectionString:connectionStr
                    )
                );

            builder.Services.AddDbContext<DataDbContext>(
                options => options.UseFirebirdEmbedded(
                    embeddedOptionsAction: opt =>
                    {
                        opt.DatabaseName = "DATA.FDB";
                        opt.DatabaseDirectory = new EmbeddedDatabaseDirectory(Path.Combine(builder.Environment.ContentRootPath, "App_Data"));
                    }
                    //,connectionString:connectionStr
                )
            );

            //var connection = new FbConnectionStringBuilder("database=localhost:data.fdb;user=sysdba;password=masterkey");
            //builder.Services.AddDbContext<DataDbContext>(
            //    options => options.UseFirebird(connection.ToString()
            //        //, opt => {
            //        //   opt.WithExplicitParameterTypes(false);
            //        //   opt.WithExplicitStringLiteralTypes(false);
            //        //}
            //        )
            //);

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