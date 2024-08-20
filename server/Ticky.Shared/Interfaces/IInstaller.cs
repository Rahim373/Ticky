using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ticky.Shared.Interfaces;

public interface IInstaller
{
    void InstallService(IServiceCollection services, IConfiguration configuration);
}
