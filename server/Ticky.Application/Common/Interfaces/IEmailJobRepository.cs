using Ticky.Domain.Entities;
using Ticky.Domain.Enums;

namespace Ticky.Application.Common.Interfaces;

public interface IEmailJobRepository
{
    Task InsertEmailJobAsync(EmailJob emailJob, CancellationToken cancellationToken = default);
}
