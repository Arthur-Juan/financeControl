using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.EFCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<User> Users { get; set; }
}