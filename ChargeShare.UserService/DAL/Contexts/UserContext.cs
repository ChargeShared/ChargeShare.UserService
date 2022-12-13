using ChargeShare.UserService.DAL.Configurations;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace ChargeShare.UserService.DAL.Contexts;

public class UserContext : DbContext
{
    private string connectionString = "TempDB";

    public DbSet<ChargeSharedUserModel> Users { get; set; }


    public UserContext()
    {

    }

    public UserContext(DbContextOptions<UserContext> context) : base(context)
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}