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
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            // Check if the entity has a property of type Guid (Id)
            var guidProperty = entityType.FindProperty("Id");
            if (guidProperty != null && guidProperty.ClrType == typeof(Guid))
            {
                // Configure the PostgreSQL data type to "uuid" for the Guid property
                guidProperty.SetColumnType("uuid");
            }

            var createdAt = entityType.FindProperty("CreatedAt");
            var updatedAt = entityType.FindProperty("UpdatedAt");

            if (createdAt != null && createdAt.ClrType == typeof(DateTime))
            {
                createdAt.SetColumnType("timestamp");
            }
            if (updatedAt != null && updatedAt.ClrType == typeof(DateTime))
            {
                updatedAt.SetColumnType("timestamp");
            }
            
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(string))
                {
                    // Configure the PostgreSQL data type to "text" for string properties
                    property.SetColumnType("text");
                }
            }
        }
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new SpentConfiguration());
    }
}