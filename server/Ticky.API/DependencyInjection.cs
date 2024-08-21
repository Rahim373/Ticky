using System.Reflection;
using Ticky.API.Common;
using Ticky.API.Middlewares;
using Ticky.Application.Common.Interfaces;

namespace Ticky.API;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            options.EnableAnnotations();
        });

        services.AddProblemDetails();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

    public static void UsePresentation(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseExceptionHandler();
    }
}
