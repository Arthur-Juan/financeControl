using DotNet.Testcontainers.Builders;
using Infra.Data.EFCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.PostgreSql;

namespace tests;

public class AppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{

  private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
    .WithImage("postgres:latest")
    .WithDatabase("finacecontrol")
    .WithUsername("postgres")
    .WithPassword("postgres")
    .WithPortBinding("5555")
    .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
    .Build();
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureTestServices(services =>
    {
      var descriptor = services.SingleOrDefault( s => s
        .ServiceType == typeof(DbContextOptions<AppDbContext>));

      if (descriptor != null)
      {
        services.Remove(descriptor);
      }

      services.AddDbContext<AppDbContext>(opt =>
      {
        opt.UseNpgsql(_dbContainer.GetConnectionString());
      });
    });
    
  }

  public Task InitializeAsync()
  {
    return _dbContainer.StartAsync();
  }

  public new Task DisposeAsync()
  {
    return _dbContainer.StopAsync();
  }
}