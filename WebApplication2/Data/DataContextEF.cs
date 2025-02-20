using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Data;

public class DataContextEF : DbContext
{
    private readonly IConfiguration _config;

    public DataContextEF(IConfiguration config)
    {
        _config = config;
    }

    
    public DbSet<User> Users { get; set; }
    public DbSet<UserJobInfo> UserJobInfo { get; set; }
    public DbSet<UserSalary> UserSalary { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                    optionsBuilder => optionsBuilder.EnableRetryOnFailure());
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>()
            .ToTable("Users")
            .HasKey(u => u.UserId);

        modelBuilder.Entity<UserSalary>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<UserJobInfo>()
            .HasKey(u => u.UserId);
    }
    
}