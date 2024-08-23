using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticky.Infrastructure.Settings;

public class AdminUserOptions
{
    public const string AdminUser = "AdminUser";

    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
