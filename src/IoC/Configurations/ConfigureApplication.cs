using Application.Features.Auth;
using Application.Interfaces.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace IoC.Configurations;

public static class ConfigureApplication
{
    public static void Configure(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRegisterUser, RegisterUser>();
        serviceCollection.AddScoped<ILoginUser, LoginUser>();
    }
}