﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Ticky.Application.Commands.Organizations;
using Ticky.Application.Commands.Workspace;
using Ticky.Domain.Constants;

namespace Ticky.API.Admin.Controllers
{
    public class OrganizationsController : BaseApiController
    {
        private readonly ISender _sender;

        public OrganizationsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("list")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<IActionResult> GetOrganizationsAsync(GetOrganizationsCommand command)
        {
            var response = await _sender.Send(command);
            return response.Match(data => new OkObjectResult(data), Problem);
        }

        [HttpPost("register-invitation")]
        [Authorize(Roles = Role.ADMIN)]
        public async Task<IActionResult> InviteToCreateOrganizationAsync(InviteOrganizationCommand command)
        {
            var response = await _sender.Send(command);
            return response.Match((data) => Ok(), Problem);
        }
    }
}
