using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firebird.Embedded.AspNetCore.Data;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(s => s.Age)
            .IsRequired(false);
        builder.Property(s => s.IsRegularStudent)
            .HasDefaultValue(true);

    }
}