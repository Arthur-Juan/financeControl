using Domain.Entities;
using Infra.Data.EFCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.EFCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Sector> Sectors { get; set; }
    public DbSet<Spent> Spents { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new SpentConfiguration());
    }
}