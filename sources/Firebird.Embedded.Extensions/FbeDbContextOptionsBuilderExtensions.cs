using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.EntityFrameworkCore.Firebird.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Firebird.Embedded.Extensions;

public static class FbeDbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseFirebirdEmbedded(this DbContextOptionsBuilder optionsBuilder,
        string connectionString="",
        Action<FbDbContextOptionsBuilder>? fbOptionsAction = null,
        Action<EmbeddedOptions>? embeddedOptionsAction = null)
    {
        EmbeddedOptions embeddedOptions= new();
        embeddedOptionsAction?.Invoke(embeddedOptions);

        //"user=SYSDBA;password=masterkey;database=Identity.fdb;servertype=1;client library=fbclient.dll"
        var fbConnection = new FbConnectionStringBuilder(connectionString);
        if (string.IsNullOrWhiteSpace(fbConnection.UserID))
            fbConnection.UserID = "SYSDBA";
        if (string.IsNullOrWhiteSpace(fbConnection.Password))
            fbConnection.Password = "masterkey";
        fbConnection.ServerType = FbServerType.Embedded;
        fbConnection.ClientLibrary = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Environment.Is64BitProcess ? "FES-x64" : "FES-x86"), "fbclient.dll");
        if (string.IsNullOrWhiteSpace(fbConnection.Database))
            fbConnection.Database = "EmbedddedDatabase.fdb";
        switch (embeddedOptions.DatabaseDirectory)
        {
           case EmbeddedDatabaseDirectory.CurrentDomainBaseDirectory:
               fbConnection.Database = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(fbConnection.Database));
                break;
            case EmbeddedDatabaseDirectory.FromConnectionString:
                //fbConnection.Database = fbConnection.Database;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        connectionString = fbConnection.ToString();
        optionsBuilder.UseFirebird(connectionString, fbOptionsAction);
        return optionsBuilder;
    }
}

public class EmbeddedOptions
{
    public EmbeddedDatabaseDirectory DatabaseDirectory { get; set; }
}

public enum EmbeddedDatabaseDirectory
{
    CurrentDomainBaseDirectory,FromConnectionString
}