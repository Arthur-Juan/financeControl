using Infra.Data.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoC.Configurations;

public static class InfraConfig
{
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        var conn = configuration.GetConnectionString("PgSql");
        services.AddDbContext<AppDbContext>(opt =>
        {   
            
            opt.UseNpgsql(conn);
        });
    }
    
}