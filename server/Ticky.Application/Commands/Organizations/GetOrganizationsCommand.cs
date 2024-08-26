using ErrorOr;
using FluentValidation;
using Ticky.Application.Common.Interfaces;
using Ticky.Shared.ViewModels.Common;
using Ticky.Shared.ViewModels.Workspace;

namespace Ticky.Application.Commands.Workspace;

public class GetOrganizationsCommand : CollectionViewModel, ITickyRequest<GridResponse<GetOrganizationResponse>>;

public class GetOrganizationsCommandValidator : AbstractValidator<GetOrganizationsCommand>
{
    public GetOrganizationsCommandValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100);
    }
}

public class GetOrganizationsCommandHandler : ITickyRequestHandler<GetOrganizationsCommand, GridResponse<GetOrganizationResponse>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetOrganizationsCommandHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<ErrorOr<GridResponse<GetOrganizationResponse>>> Handle(GetOrganizationsCommand request, CancellationToken cancellationToken)
    {
        var (total, items) = await _organizationRepository.GetOrganizationsAsync(request.PageSize, request.PageNumber - 1, cancellationToken);

        return new GridResponse<GetOrganizationResponse>
        {
            Items = items
                .Select(x => x.ToGetOrganizationResponse())
                .ToList(),
            PageNumber = request.PageNumber,
            Total = total,
            PageSize = request.PageSize
        };
    }
}