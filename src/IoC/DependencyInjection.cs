using IoC.Configurations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IoC;

public static class DependencyInjection
{
    public static void ConfigureDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        InfraConfig.Configure(serviceCollection, configuration);
        ConfigureApplication.Configure(serviceCollection);
    }
}