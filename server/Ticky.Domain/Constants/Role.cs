namespace Ticky.Domain.Constants;

public sealed class Role
{
    public const string ADMIN = "Admin";
    public const string ORG_OWNER = "OrgOwner";

    /// <summary>
    /// Only used for controller auth
    /// </summary>
    public const string MANAGERS = $"{ADMIN},${ORG_OWNER}";
}
