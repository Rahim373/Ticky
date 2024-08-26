namespace Ticky.Domain.Entities;

public class RegistrationInvitation : BaseEntity
{
    public bool IsUsed { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public bool OrganizationInvite { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime ExpiresOn { get; set; }

    public bool IsExpired()
    {
        return ExpiresOn < DateTime.UtcNow;
    }
}
