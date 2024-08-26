using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Entities;

namespace Ticky.Application.Commands.Organizations;

public record InviteOrganizationCommand(string Email) : ITickyRequest<bool>;

public class InviteOrganizationCommandValidator : AbstractValidator<InviteOrganizationCommand>
{
    public InviteOrganizationCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .EmailAddress();
    }
}

public class InviteOrganizationCommandHandler : ITickyRequestHandler<InviteOrganizationCommand, bool>
{
    private readonly IOrganizationRepository _organizationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailJobRepository _emailJobRepository;
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public InviteOrganizationCommandHandler(
        IOrganizationRepository organizationRepository,
        IUserRepository userRepository,
        IEmailJobRepository emailJobRepository,
        IEmailService emailService, 
        IUnitOfWork unitOfWork)
    {
        _organizationRepository = organizationRepository;
        _userRepository = userRepository;
        _emailJobRepository = emailJobRepository;
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> Handle(InviteOrganizationCommand request, CancellationToken cancellationToken)
    {
        var email = request.Email.ToLower();

        var user = await _userRepository.GetUserByEmailAsync(email, cancellationToken);

        if (user is not null)
        {
            var organization = await _organizationRepository.GetOrganizationByEmailAsync(email, cancellationToken);

            if (organization is not null)
            {
                if (!organization.IsActive)
                {
                    return Error.Conflict(description: "An inactive organization is already created with this email.");
                }

                return Error.Validation(description: "An organization is already created with this email.");
            }

            return Error.Conflict(description: "User already have an account without an organization");
        }

        var expiresOn = DateTime.UtcNow.AddHours(48);
        var token = $"{Guid.NewGuid().ToString()}{Guid.NewGuid().ToString()}".ToLower().Replace("-", "");
        var body = _emailService.GenerateRegisterInvitationEmail(email, token);

        var emailJob = new EmailJob(email, body);
        emailJob.MarkEmailForRegistrationInvitation(true);

        await _userRepository.InsertRegistrationInvitationAsync(email, token, expiresOn, true, cancellationToken);
        await _emailJobRepository.InsertEmailJobAsync(emailJob, cancellationToken);

        await _unitOfWork.CommitChangesAsync();

        //TODO Publish to event...
        return true;
    }
}