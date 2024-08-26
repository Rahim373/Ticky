using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Ticky.Application.Commands.Auth;
using Ticky.Shared.ViewModels.Auth;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Ticky.API.Admin.Controllers;

[AllowAnonymous]
public class AuthController : BaseApiController
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    /// <summary>
    /// Return JWT token
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("/token")]
    [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(TokenResponse))]
    public async Task<IActionResult> GetTokenAsync(GetTokenCommand command)
    {
        var response = await _sender.Send(command);
        return response.Match(x => new OkObjectResult(x), Problem);
    }

    [HttpPost]
    public async Task<IActionResult> AcceptInvitation([FromQuery]string token)
    {
        var response = await _sender.Send(new AcceptInvitationCommand(token));
        return response.Match((data) => Ok(), Problem);
    }
}
