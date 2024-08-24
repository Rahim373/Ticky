using Ticky.API.Admin;
using Ticky.Application;
using Ticky.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services
    .AddPresentation(configuration)
    .AddInfrastructure(configuration)
    .AddApplication(configuration);

var app = builder.Build();
app.UsePresentation()
    .UseInfrastructure();

app.Run();
