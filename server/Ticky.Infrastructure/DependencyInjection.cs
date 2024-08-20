using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ticky.Application.Interfaces.Persistence.Repositories;
using Ticky.Infrastructure.Persistence;
using Ticky.Infrastructure.Persistence.Repositories;

namespace Ticky.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(option =>
        {
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IEventRepository, EventRepository>();

        return services;
    }
}
