using Ticky.Domain.Entities;

namespace Ticky.Application.Common.Interfaces;

public interface IOrganizationRepository
{
    Task<(int Total, List<Organization> Items)> GetOrganizationsAsync(int take, int skip, CancellationToken cancellationToken = default);
}
