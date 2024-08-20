using Ticky.API;
using Ticky.Application;
using Ticky.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddPresentation(configuration)
    .AddInfrastructure(configuration)
    .AddApplication(configuration);

var app = builder.Build();
app.UsePresentation();
app.Run();
