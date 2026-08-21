using GS1.Database.Context;
using GS1.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace GS1.ApiService;

public static class Extensions
{
    public static IServiceCollection AddGS1DbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("gs1");

        services.AddDbContext<GS1DbContext>(serviceProvider => serviceProvider
            .UseSqlServer(connectionString));

        return services;
    }

    public static async Task<WebApplication> ApplyMigrations(this WebApplication builder)
    {
        using var scope = builder.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<GS1DbContext>();

        await dbContext.Database.MigrateAsync(default);

        return builder;
    }

    public static async Task<WebApplication> SeedDatabase(this WebApplication builder)
    {
        using var scope = builder.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<GS1DbContext>();

        if (!await dbContext.Companies.AnyAsync())
        {
            await dbContext.Companies.AddAsync(new Company { Code = "NEST", Name = "Nestle" });
            await dbContext.Companies.AddAsync(new Company { Code = "MEUN", Name = "Melkunie" });
            await dbContext.Companies.AddAsync(new Company { Code = "AHOL", Name = "Ahold" });

            await dbContext.SaveChangesAsync();
        }

        return builder;
    }
}
