using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Constants;
using Ticky.Domain.Entities;

namespace Ticky.Application.Commands.Auth;

public record AcceptInvitationCommand(string Token, string Email, string Password, string ConfirmPassword) : ITickyRequest<bool>;

public class AcceptInvitationCommandValidator : AbstractValidator<AcceptInvitationCommand>
{
    public AcceptInvitationCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty()
            .NotNull()
            .Length(64);

        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .Equal(x => x.ConfirmPassword);
    }
}

public class AcceptInvitationCommandHandler : ITickyRequestHandler<AcceptInvitationCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptInvitationCommandHandler(
        IUserRepository userRepository,
        UserManager<ApplicationUser> userManager,
        IOrganizationRepository organizationRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _organizationRepository = organizationRepository;
        _unitOfWork = unitOfWork;
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

        if (!invitation.Email.Equals(request.Email.ToLower()))
            return Error.Validation(description: "This is not a valid token for this email. Please use valid email.");

        var existingUser = await _userRepository.GetUserByEmailAsync(invitation.Email, cancellationToken);

        if (existingUser is not null)
        {
            if (!invitation.OrganizationInvite)
            {
                return Error.Forbidden(description: "You are already registered. Please login to explore Ticky.");
            }

            var organization = await _organizationRepository.GetOrganizationByEmailAsync(invitation.Email, cancellationToken);

            if (organization is not null)
            {
                return Error.Unexpected("You already own an organization.");
            }

            existingUser.PendingOrgCreation = true;
            await _userRepository.UpdateUserAsync(existingUser.Id,
                x => x.SetProperty(y => y.PendingOrgCreation, true), cancellationToken);

            await _unitOfWork.CommitChangesAsync();
            return true;
        }

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

        await _userManager.AddToRolesAsync(applicationUser, [Role.ORG_OWNER, Role.USER]);

        await _userRepository.ChangeInvitationUsedStatusAsync(invitation.Id, true, cancellationToken);

        return true;
    }
}
