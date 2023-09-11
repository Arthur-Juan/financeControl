using System.Text.Json.Serialization;
using Api.Extensions;
using Infra.Data.EFCore;
using IoC;
using IoC.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
    
    x.SwaggerDoc("v1", new OpenApiInfo{Title = "Basket-API"});
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT you know",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer"
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});




//Dependency Injection
builder.Services.ConfigureDependencies(configuration: builder.Configuration);
builder.Services.AddJwt(configuration: builder.Configuration);

var app = builder.Build();

//RUN MIGRATIONS

// Migrate latest database changes during startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();
    
    // Here is the migration executed
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.

app.StartErrorHandling();

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
    
}