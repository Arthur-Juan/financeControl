using Application.Features.Auth;
using Application.Features.Department;
using Application.Interfaces.Auth;
using Application.Interfaces.Department;
using Microsoft.Extensions.DependencyInjection;

namespace IoC.Configurations;

public static class ConfigureApplication
{
    public static void Configure(IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IRegisterUser, RegisterUser>();
        serviceCollection.AddScoped<ILoginUser, LoginUser>();
        serviceCollection.AddScoped<ICreateDepartment, CreateDepartment>();
    }
}