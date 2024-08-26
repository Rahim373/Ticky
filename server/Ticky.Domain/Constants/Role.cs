namespace Ticky.Domain.Constants;

public static class Role
{
    public const string ADMIN = "Admin";
    public const string ORG_OWNER = "OrgOwner";
    public const string USER = "User";

    /// <summary>
    /// Only used for controller auth
    /// </summary>
    public const string MANAGERS = $"{ADMIN},${ORG_OWNER}";
}
