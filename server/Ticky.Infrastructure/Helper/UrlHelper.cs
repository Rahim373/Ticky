using Azure.Core;
using Microsoft.AspNetCore.Http;
using Throw;

namespace Ticky.Infrastructure.Helper;

public static class UrlHelper
{
    public static string GetBaseUrl(this HttpContext context)
    {
        context.ThrowIfNull(nameof(context));
        var request = context.Request;

        var uriBuilder = new UriBuilder(request.Scheme, request.Host.Host, request.Host.Port ?? -1);
        if (uriBuilder.Uri.IsDefaultPort)
        {
            uriBuilder.Port = -1;
        }

        return uriBuilder.Uri.AbsoluteUri;
    }

    public static string GenerateInvitationAcceptUrl(this HttpContext context, string token)
    {
        var baseUrl = GetBaseUrl(context).Trim('/');
        return $"{baseUrl}/auth/accept-invitation?token={token}";
    }
}
