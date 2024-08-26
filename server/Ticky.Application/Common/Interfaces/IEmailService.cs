namespace Ticky.Application.Common.Interfaces;

public interface IEmailService
{
    string GenerateRegisterInvitationEmail(string email, string token);
}
