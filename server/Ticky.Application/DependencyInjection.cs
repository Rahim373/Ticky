using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ticky.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(option =>
        {
            option.RegisterServicesFromAssembly(ApplicationAssembly.Assembly);
        });

        return services;
    }
}
