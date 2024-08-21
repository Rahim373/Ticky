namespace Ticky.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}
