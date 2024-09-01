using ErrorOr;
using FluentValidation;
using Ticky.Application.Common.Interfaces;
using Ticky.Domain.Entities;

namespace Ticky.Application.Commands.Organizations;

public record InviteOrganizationCommand(string Email, bool InviteOrgOnwer) : ITickyRequest<bool>;

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

        if (user is not null && !request.InviteOrgOnwer)
        {
            return Error.Conflict(description: "This user is already in the system.");
        }

        if (user is not null && request.InviteOrgOnwer)
        {
            var organization = await _organizationRepository.GetOrganizationByEmailAsync(email, cancellationToken);

            if (organization is not null)
            {
                if (!organization.IsActive)
                {
                    return Error.Conflict(description: "This email is already an owner of an inactive organization.");
                }

                return Error.Validation(description: "This email already owns an organization.");
            }

            await _userRepository.UpdateUserAsync(user.Id, 
                x => x.SetProperty(y => y.PendingOrgCreation, true)
                , cancellationToken);
        }

        var expiresOn = DateTime.UtcNow.AddHours(48);
        var token = $"{Guid.NewGuid().ToString()}{Guid.NewGuid().ToString()}".ToLower().Replace("-", "");
        var body = _emailService.GenerateRegisterInvitationEmail(email, token);

        var emailJob = new EmailJob(email, body);
        emailJob.MarkEmailForRegistrationInvitation(request.InviteOrgOnwer);

        await _userRepository.InsertRegistrationInvitationAsync(email, token, expiresOn, true, cancellationToken);
        await _emailJobRepository.InsertEmailJobAsync(emailJob, cancellationToken);

        await _unitOfWork.CommitChangesAsync();

        return true;
    }
}