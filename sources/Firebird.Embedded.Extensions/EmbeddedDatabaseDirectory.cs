using FirebirdSql.Data.FirebirdClient;

namespace Firebird.Embedded.Extensions;

public class EmbeddedDatabaseDirectory: IEmbeddedDatabaseDirectory
{
    protected readonly string _DatabaseDirectory;

    public EmbeddedDatabaseDirectory(string databaseDirectory)
    {
        _DatabaseDirectory = databaseDirectory;
    }

    public string GetDatabasePath(string connectionstring = "")
    {
        var fbConnection = new FbConnectionStringBuilder(connectionstring);
        return Path.Combine(_DatabaseDirectory, Path.GetFileName(fbConnection.Database));
    }

    private static CurrentDomainBaseDirectoryClass? _CurrentDomainBaseDirectory;
    protected class CurrentDomainBaseDirectoryClass: IEmbeddedDatabaseDirectory
    {
        public string GetDatabasePath(string connectionstring)
        {
            var fbConnection = new FbConnectionStringBuilder(connectionstring);
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Path.GetFileName(fbConnection.Database));
        }
    }
    public static IEmbeddedDatabaseDirectory CurrentDomainBaseDirectory => _CurrentDomainBaseDirectory ??= new();


    protected class FromConnectionStringClass : IEmbeddedDatabaseDirectory
    {
        public string GetDatabasePath(string connectionstring)
        {
            var fbConnection = new FbConnectionStringBuilder(connectionstring);
            return fbConnection.Database;
        }
    }
    private static FromConnectionStringClass? _FromConnectionString;
    public static IEmbeddedDatabaseDirectory FromConnectionString => _FromConnectionString ??= new();

}