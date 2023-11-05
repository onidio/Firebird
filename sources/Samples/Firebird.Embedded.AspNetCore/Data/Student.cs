namespace Firebird.Embedded.AspNetCore.Data;

public class Student
{
    public int? Age { get; set; }
    public bool IsRegularStudent { get; set; }
    public string Id { get; set; } = null!;
}