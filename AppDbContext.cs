using Microsoft.EntityFrameworkCore;
public class AppDbContext : DbContext
{
    public DbSet<Task> Tasks {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder options)=> options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TodoEfCoreDb;Trusted_Connection=True;");

    public void EnsureDatabaseCreated()
    {
        Database.EnsureCreated();
    }
}