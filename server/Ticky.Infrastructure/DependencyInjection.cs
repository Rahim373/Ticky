using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Entities;
using Ticky.Infrastructure.Persistence;
using Ticky.Infrastructure.Persistence.DataSeed;
using Ticky.Infrastructure.Persistence.Repositories;
using Ticky.Infrastructure.Services;
using Ticky.Shared.Settings;

namespace Ticky.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApplicationOptions>(configuration);
        var appSettings = configuration.Get<ApplicationOptions>();

        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(appSettings!.ConnectionStrings.DefaultConnection);
        });

        services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
        {
            options.User.RequireUniqueEmail = true;
        }).AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            var jwtConfig = appSettings!.JwtConfig;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidAudience = jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddScoped<DbMigrationService>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

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
