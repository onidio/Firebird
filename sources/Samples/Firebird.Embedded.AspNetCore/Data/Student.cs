using System.ComponentModel.DataAnnotations.Schema;

namespace Firebird.Embedded.AspNetCore.Data;

[Table("STUDENTS")]
public class Student
{
    public int? Age { get; set; }
    public bool IsRegularStudent { get; set; }
    public string? Id { get; set; }
}