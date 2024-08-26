namespace Ticky.Shared.Settings;

public class ApplicationOptions
{
    public Logging Logging { get; set; }
    public string AllowedHosts { get; set; }
    public Connectionstrings ConnectionStrings { get; set; }
    public bool AutoMigration { get; set; }
    public DemoUser[] SeedUsers { get; set; }
    public Jwtconfig JwtConfig { get; set; }
    public string[] CorsOrigins { get; set; } = []; 
}

public class DemoUser
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string[] Roles { get; set; }
}

public class Logging
{
    public Loglevel LogLevel { get; set; }
}

public class Loglevel
{
    public string Default { get; set; }
    public string MicrosoftAspNetCore { get; set; }
}

public class Connectionstrings
{
    public string DefaultConnection { get; set; }
}

public class Adminuser
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class Jwtconfig
{
    public string Secret { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int AccessTokenExpiryTimeMins { get; set; }
    public int RefreshTokenExpiryTimeMins { get; set; }
}
