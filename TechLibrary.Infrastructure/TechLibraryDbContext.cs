using Microsoft.EntityFrameworkCore;
using TechLibrary.Domain.Entities;

namespace TechLibrary.Infrastructure;
public class TechLibraryDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\Workspace\\TechLibraryDb.db");
    }
}
