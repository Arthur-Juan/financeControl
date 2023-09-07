using Infra.Data.EFCore;
using Microsoft.Extensions.DependencyInjection;

namespace tests;

public abstract class BasicTest : IClassFixture<AppFactory>
{
    private readonly AppDbContext _context;
    protected BasicTest(AppFactory factory)
    {
        var scope = factory.Services.CreateScope();
        _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }

    
}