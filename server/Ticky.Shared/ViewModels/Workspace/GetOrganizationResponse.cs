using Ticky.Domain.Entities;

namespace Ticky.Shared.ViewModels.Workspace;

public class GetOrganizationResponse
{
    public string Name { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public bool IsActive { get; set; }
    public OrganizationOwner? Owner { get; set; }
}

public record OrganizationOwner(Guid Id, string Email);

public static class OrganizationsExtensions
{
    public static GetOrganizationResponse ToGetOrganizationResponse(this Organization organization)
    {
        return new GetOrganizationResponse
        {
            CreatedOn = organization.CreatedOn,
            Name = organization.Name,
            IsActive = organization.IsActive,
            UpdatedOn = organization.UpdatedOn,
            Owner = organization.Owner is not null
                ? new OrganizationOwner(organization.Owner!.Id, organization.Owner!.Email)
                : null
        };
    }
}