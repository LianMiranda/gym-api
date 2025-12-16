using Gym.Domain.Interfaces.Repositories;

namespace Gym.Domain.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    
    Task SaveAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync(); 
}