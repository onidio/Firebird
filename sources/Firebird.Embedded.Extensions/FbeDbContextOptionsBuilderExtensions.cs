using FirebirdSql.Data.FirebirdClient;
using FirebirdSql.EntityFrameworkCore.Firebird.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Firebird.Embedded.Extensions;

public static class FbeDbContextOptionsBuilderExtensions
{
    public static DbContextOptionsBuilder UseFirebirdEmbedded(
        this DbContextOptionsBuilder optionsBuilder,
        Action<FbDbContextOptionsBuilder>? fbOptionsAction = null,
        Action<EmbeddedOptions>? embeddedOptionsAction = null,
        string connectionString = "")
    {
        EmbeddedOptions embeddedOptions= new();
        embeddedOptionsAction?.Invoke(embeddedOptions);

        var fbConnection = new FbConnectionStringBuilder(connectionString);
        if (string.IsNullOrWhiteSpace(fbConnection.UserID))
            fbConnection.UserID = "SYSDBA";
        if (string.IsNullOrWhiteSpace(fbConnection.Password))
            fbConnection.Password = "masterkey";
        if (string.IsNullOrWhiteSpace(fbConnection.Charset))
            fbConnection.Charset = "utf8";
        fbConnection.ServerType = FbServerType.Embedded;
        fbConnection.ClientLibrary = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Environment.Is64BitProcess ? "FES-x64" : "FES-x86"), "fbclient.dll");
        if (embeddedOptions.DatabaseName != null)
        {
            fbConnection.Database = embeddedOptions.DatabaseName;
        }
        if (string.IsNullOrWhiteSpace(fbConnection.Database))
            fbConnection.Database = "EmbedddedDatabase.fdb";
        fbConnection.Database = embeddedOptions.DatabaseDirectory?.GetDatabasePath(fbConnection.ToString());
       
        connectionString = fbConnection.ToString();
        optionsBuilder.UseFirebird(connectionString, fbOptionsAction);
        return optionsBuilder;
    }
}