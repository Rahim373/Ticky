using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Entities;

namespace Ticky.Application.Commands.Auth;

public record AcceptInvitationCommand(string Token) : ITickyRequest<bool>;

public class AcceptInvitationCommandValidator : AbstractValidator<AcceptInvitationCommand>
{
    public AcceptInvitationCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty()
            .NotNull()
            .Length(64);
    }
}

public class AcceptInvitationCommandHandler : ITickyRequestHandler<AcceptInvitationCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public AcceptInvitationCommandHandler(IUserRepository userRepository,
        UserManager<ApplicationUser> userManager)
    {
        this._userRepository = userRepository;
        this._userManager = userManager;
    }

    public async Task<ErrorOr<bool>> Handle(AcceptInvitationCommand request, CancellationToken cancellationToken)
    {
        var token = request.Token.ToLower();
        RegistrationInvitation? invitation = await _userRepository.GetRegistrationInvitationTokenAsync(token, cancellationToken);

        if (invitation is null)
            return Error.Validation(description: "Invalid invitation token.");

        if (invitation.IsExpired())
            return Error.Validation(description: "Token inviation token is expired.");

        if (invitation.IsUsed)
            return Error.Validation(description: "Token is already used.");

        var existingUser = await _userRepository.GetUserByEmailAsync(invitation.Email, cancellationToken);

        if (existingUser is not null)
            return Error.Forbidden(description: "You already registered. Please login to explore Ticky.");

        var applicationUser = new ApplicationUser
        {
            UserName = invitation.Email,
            Email = invitation.Email,
            NormalizedEmail = invitation.Email.ToUpper(),
            NormalizedUserName = invitation.Email.ToUpper(),
            EmailConfirmed = true,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
        };

        var createResult = await _userManager.CreateAsync(applicationUser);

        if (!createResult.Succeeded)
            return Error.Validation(description: "User cannot be created.");

        await _userRepository.ChangeInvitationUsedStatusAsync(true, cancellationToken);

        return true;
    }
}
