using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firebird.Embedded.AspNetCore.Data;

public class StudentDataConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasData(
            new Student
            {
                Id = Guid.NewGuid().ToString(),
                Age = 30
            },
            new Student
            {
                Id = Guid.NewGuid().ToString(),
                Age = 25
            }
        );
    }
}