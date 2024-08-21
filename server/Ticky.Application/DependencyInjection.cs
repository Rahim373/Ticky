using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ticky.Application.Common.Behaviors;

namespace Ticky.Application;
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddValidatorsFromAssembly(ApplicationAssembly.Assembly);

        services.AddMediatR(option =>
        {
            option.RegisterServicesFromAssembly(ApplicationAssembly.Assembly);
            option.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        return services;
    }
}
