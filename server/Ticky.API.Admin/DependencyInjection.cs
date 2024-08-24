using System.Reflection;
using Ticky.API.Admin.Common;
using Ticky.API.Admin.Middlewares;
using Ticky.Application.Common.Interfaces;
using Ticky.Shared.Settings;

namespace Ticky.API.Admin;

public static class DependencyInjection
{
    private static string DefaultCorsPolicy = "DefaultCorsPolicy";

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

        services.AddCors(options =>
        {
            var origins = configuration.Get<ApplicationOptions>()?.CorsOrigins;

            if (origins is not null)
            {
                options.AddPolicy(DefaultCorsPolicy, policy =>
                {
                    policy.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(origins);
                });
            }
        });

        return services;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(DefaultCorsPolicy);

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.UseExceptionHandler();

        return app;
    }
}
