using Ticky.Domain.Enums;

namespace Ticky.Domain.Entities;

public class EmailJob : BaseEntity
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public EmailType Type { get; set; }
    public DateTime? SentOn { get; set; }

    public EmailJob()
    {
        
    }

    public EmailJob(string to, string body)
    {
        To = to;
        Body = body;
    }

    public void MarkEmailForRegistrationInvitation(bool orgInvitation)
    {
        Subject = orgInvitation ?
            "Register to Ticky. Free your boundary."
            : "Register to Ticky";
        Type = EmailType.RegistrationInvitation;
    }
}
