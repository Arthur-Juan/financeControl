using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infra.Data.EFCore;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // var configuration = new ConfigurationBuilder();
        // var build = configuration.Build();
        var builder = new DbContextOptionsBuilder<AppDbContext>();

        // var connectionString = build.GetConnectionString("DefaultConnection");
        builder.UseSqlServer("User ID=postgres;Password=S3cur3P@ssW0rd!;Host=db;Port=5432;Database=financecontrol;Pooling=true;");

        // Cria o contexto
        return new AppDbContext(builder.Options);
    }
}