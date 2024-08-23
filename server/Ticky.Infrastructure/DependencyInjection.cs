using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Entities;
using Ticky.Infrastructure.Persistence;
using Ticky.Infrastructure.Persistence.DataSeed;
using Ticky.Infrastructure.Persistence.Repositories;
using Ticky.Infrastructure.Settings;

namespace Ticky.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ApplicationDbContext>();

        services.Configure<AdminUserOptions>(configuration.GetSection(AdminUserOptions.AdminUser));
        services.AddScoped<DbMigrationService>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IEventRepository, EventRepository>();

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        MigrateAndSeedData(app);

        return app;
    }

    private static void MigrateAndSeedData(WebApplication app)
    {
        var serviceFactory = app.Services.GetService<IServiceScopeFactory>();

        using var scope = serviceFactory!.CreateScope();

        var seedService = scope.ServiceProvider.GetService<DbMigrationService>();

        seedService!.MigrateDbAsync().Wait();
        seedService!.SeedDatabaseAsync().Wait();
    }
}
