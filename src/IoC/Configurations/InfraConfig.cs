using Domain.Interfaces;
using Infra.Data.EFCore;
using Infra.Services;
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

        services.AddScoped<ICryptoService, BcryptAdapter>();
    }
    
}