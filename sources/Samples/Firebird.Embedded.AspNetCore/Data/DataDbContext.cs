using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firebird.Embedded.AspNetCore.Data;

public class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .Property(s => s.Age)
            .IsRequired(false);
        modelBuilder.Entity<Student>()
            .Property(s => s.IsRegularStudent)
            .HasDefaultValue(true);

        //modelBuilder.Entity<Student>()
        //    .HasData(
        //        new Student
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            Age = 30
        //        },
        //        new Student
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            Age = 25
        //        }
        //    );
    }

    //Install-Package Microsoft.EntityFrameworkCore.Sqlite

    public DbSet<Student> Students { get; set; }
}
