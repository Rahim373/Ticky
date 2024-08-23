using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Constants;
using Ticky.Domain.Entities;
using Ticky.Infrastructure.Settings;

namespace Ticky.Infrastructure.Persistence.DataSeed;

public class DbMigrationService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly AdminUserOptions _adminUserOption;

    public DbMigrationService(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<AdminUserOptions> options,
        IConfiguration configuration)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _adminUserOption = options.Value;
    }

    public async Task MigrateDbAsync()
    {
        var autoMigration = _configuration.GetValue<bool?>("AutoMigration").GetValueOrDefault(false);

        if (autoMigration)
        {
            var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();

            if (pendingMigrations.Any())
            {
                await _context.Database.MigrateAsync();
            }
        }
    }

    public async Task SeedDatabaseAsync()
    {
        await CreateRolesAsync();
        await CreateAdminUserAsync();
    }

    private async Task CreateAdminUserAsync()
    {
        if ((await _userManager.FindByEmailAsync(_adminUserOption.Email)) is var user
            && user is null)
        {
            user = new ApplicationUser
            {
                Email = _adminUserOption.Email,
                UserName = _adminUserOption.Email,
                NormalizedEmail = _adminUserOption.Email,
                NormalizedUserName = _adminUserOption.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            await _userManager.CreateAsync(user);
            await _userManager.AddPasswordAsync(user, _adminUserOption.Password);
            await _userManager.AddToRoleAsync(user, Role.ADMIN);
        }
    }

    private async Task CreateRolesAsync()
    {
        List<string> roles = [Role.ADMIN, Role.ORG_OWNER];

        foreach (var role in roles)
        {
            var roleExists = await _roleManager.RoleExistsAsync(role);

            if (!roleExists)
            {
                await _roleManager.CreateAsync(new ApplicationRole
                {
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = role,
                    NormalizedName = role.ToUpper(),
                });
            }
        }
    }
}
