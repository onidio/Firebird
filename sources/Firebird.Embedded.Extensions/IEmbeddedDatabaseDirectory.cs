namespace Firebird.Embedded.Extensions;

public interface IEmbeddedDatabaseDirectory
{
    string GetDatabasePath(string connectionstring="");
}