using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Firebird.Embedded.AspNetCore.Data;

public class DataDbContext : DbContext
{
    public DataDbContext(DbContextOptions<DataDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StudentConfiguration());
        modelBuilder.ApplyConfiguration(new StudentDataConfiguration());
    }

    //Install-Package Microsoft.EntityFrameworkCore.Sqlite

    public DbSet<Student> Students { get; set; } = null!;
}