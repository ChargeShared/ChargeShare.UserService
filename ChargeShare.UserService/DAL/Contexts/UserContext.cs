using ChargeShare.UserService.DAL.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace ChargeShare.UserService.DAL.Contexts;

public class UserContext : IdentityDbContext<ChargeSharedUserModel, IdentityRole<int>, int>
{
    private string connectionString = "TempDB";
    private string connectionString2 = "Server=localhost;Database=UserDB;Trusted_Connection=True;";

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
            optionsBuilder.UseSqlServer(connectionString: connectionString2);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}