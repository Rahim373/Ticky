using Ticky.Domain.Entities;

namespace Ticky.Application.Common.Interfaces;

public interface IDiscountRepository
{
    public Task<Discount> GetDiscountAsync(Guid id, CancellationToken cancellationToken = default);
    public Task<IList<Discount>> GetDiscountsAsync(CancellationToken cancellationToken = default);
    public Task<Discount> CreateDiscountAsync(Discount discount, CancellationToken cancellationToken = default);
    public Task<Discount> UpdateDiscountAsync(Guid id, Discount discount, CancellationToken cancellationToken = default);
    public Task<Discount> DeleteDiscountAsync(Guid id, Discount discount, CancellationToken cancellationToken = default);
}
