using Microsoft.AspNetCore.Http;
using Ticky.Application.Common.Interfaces;
using Ticky.Infrastructure.Helper;

namespace Ticky.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IHttpContextAccessor _httpContext;

    public EmailService(IHttpContextAccessor httpContext)
    {
        _httpContext = httpContext;
    }

    public string GenerateRegisterInvitationEmail(string email, string token)
    {
        var url = _httpContext.HttpContext!.GenerateInvitationAcceptUrl(token);

        return
        @$"Hello {email},
           You got an invitation to join on Ticky. Click on the link below to join.
            <a href=""{url}"">Join Ticky</a>
            </br>
            {url}";
    }
}
