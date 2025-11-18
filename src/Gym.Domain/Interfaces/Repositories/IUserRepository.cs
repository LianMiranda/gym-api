using Gym.Domain.Entities;

namespace Gym.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task SaveAsync(User user);
    void Delete(User user);
    void Update(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<IEnumerable<User>?> GetAllAsync();
    Task<bool> EmailExistsAsync(string email);
}