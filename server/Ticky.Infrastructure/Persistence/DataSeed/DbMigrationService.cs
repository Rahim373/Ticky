using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Ticky.Domain.Constants;
using Ticky.Domain.Entities;
using Ticky.Shared.Settings;

namespace Ticky.Infrastructure.Persistence.DataSeed;

public class DbMigrationService
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly ApplicationOptions _appOptions;

    public DbMigrationService(
        ApplicationDbContext context,
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager,
        IOptions<ApplicationOptions> options)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _appOptions = options.Value;
    }

    public async Task MigrateDbAsync()
    {
        if (_appOptions.AutoMigration)
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
        if (_appOptions.SeedUsers is null)
            return;

        foreach (var seedUser in _appOptions.SeedUsers)
        {
            if (IsSeedUserInvalid(seedUser))
                continue;

            if ((await _userManager.FindByEmailAsync(seedUser.Email)) is var user
                && user is null)
            {
                user = new ApplicationUser
                {
                    Email = seedUser.Email,
                    UserName = seedUser.Email,
                    NormalizedEmail = seedUser.Email,
                    NormalizedUserName = seedUser.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                await _userManager.CreateAsync(user);
                await _userManager.AddPasswordAsync(user, seedUser.Password);

            }

            foreach (var role in seedUser.Roles)
            {
                if (TryGetSanitizedRole(role, out var userRole))
                {
                    var isAssignedToRole = await _userManager.IsInRoleAsync(user, role);

                    if (!isAssignedToRole)
                        await _userManager.AddToRoleAsync(user, userRole);
                }
            }
        }
    }

    private static bool IsSeedUserInvalid(DemoUser seedUser)
    {
        return seedUser is null
            || string.IsNullOrEmpty(seedUser.Email)
            || string.IsNullOrEmpty(seedUser.Password)
            || seedUser.Roles is null
            || seedUser.Roles.Length is 0;
    }

    private static bool TryGetSanitizedRole(string role, out string sanitizedRole)
    {
        sanitizedRole = role switch
        {
            Role.ADMIN => Role.ADMIN,
            Role.ORG_OWNER => Role.ORG_OWNER,
            Role.USER => Role.USER,
            _ => string.Empty
        };

        return !string.IsNullOrEmpty(sanitizedRole);
    }

    private async Task CreateRolesAsync()
    {
        List<string> roles =
        [
            Role.ADMIN,
            Role.ORG_OWNER,
            Role.USER
        ];

        var roleIdDisctionary = new Dictionary<string, Guid>();

        foreach (var role in roles)
        {
            var parentRole = GetParentRole(role);
            var dbRole = await _context.Roles.Where(x => x.Name == role).FirstOrDefaultAsync();

            if (dbRole is null)
            {
                Guid? parentId = null;
                if (!string.IsNullOrEmpty(parentRole))
                {
                    roleIdDisctionary.TryGetValue(parentRole, out var parsedParentId);
                    parentId = parsedParentId == Guid.Empty ? null : parsedParentId;
                }

                dbRole = new ApplicationRole
                {
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = role,
                    NormalizedName = role.ToUpper(),
                    Extends = parentId
                };

                await _context.Roles.AddAsync(dbRole);
                await _context.SaveChangesAsync();

            }

            roleIdDisctionary.Add(role, dbRole!.Id);
        }
    }

    private static string? GetParentRole(string role)
    {
        return role switch
        {
            Role.ADMIN => null,
            Role.ORG_OWNER => Role.ADMIN,
            Role.USER => Role.ORG_OWNER,
            _ => null
        };
    }
}
