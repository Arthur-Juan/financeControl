using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using Infra.Data;
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
        ConfigureJwt(services, configuration);

        services.AddScoped<ICryptoService, BcryptAdapter>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void ConfigureJwt(IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<IJwtConfiguration>(x => 
            new JwtConfiguration(
            configuration.GetSection("JwtConfig:Secret").ToString(),
            configuration.GetSection("JwtConfig:Issuer").ToString(),
            configuration.GetSection("JwtConfig:Audience").ToString()
        ));
        serviceCollection.AddScoped<ITokenService, JwtAdapter>();
    }
}