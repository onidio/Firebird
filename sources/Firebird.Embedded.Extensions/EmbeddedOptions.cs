namespace Firebird.Embedded.Extensions;

public class EmbeddedOptions
{
    public IEmbeddedDatabaseDirectory? DatabaseDirectory { get; set; }
    public string? DatabaseName { get; set; }
}