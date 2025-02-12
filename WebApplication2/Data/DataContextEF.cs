using WebApplication2.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Data;

public class DataContextEF : DbContext
{
    public DataContextEF(DbContextOptions<DataContextEF> options): base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<UserJobInfo> UserJobInfo { get; set; }
    public DbSet<UserSalary> UserSalary { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        if (!options.IsConfigured)
        {
            // options.UseSqlServer("Server=localhost;Database=DotNetCourseDatabase;Trusted_connection=false;TrustServerCertificate=True;User Id=sa;Password=SQLConnect1!;",
            options.UseSqlServer("Server=localhost;Database=DotNetCourseDatabase;Trusted_Connection=true;TrustServerCertificate=true;",
                options => options.EnableRetryOnFailure());
        }
    }
    
    
    
}