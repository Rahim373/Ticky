using System.Security.Claims;
using Ticky.Application.Common.Interfaces;

namespace Ticky.API.Admin.Common;

public class UserContext(IHttpContextAccessor _httpContext) : IUserContext
{
    public Guid? GetUserId()
    {
        if (_httpContext.HttpContext is null 
            || !_httpContext.HttpContext.User.Identity.IsAuthenticated)
        {
            return null;
        }

        var userId = _httpContext.HttpContext!.User.Claims
            .FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)
            .Value;

        if (Guid.TryParse(userId, out Guid id))
        {
            return id;
        }
                
        return null;
    }
}
