using Gym.Domain.Entities;

namespace Gym.Domain.Interfaces.Repositories;

public interface IUserRepository
{
    Task CreateAsync(User user);
    void Delete(User user);
    void Update(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<(IEnumerable<User>? users, int totalCount, int page, int pageSize)> GetAllAsync(int page, int take);
    Task<bool> EmailExistsAsync(string email);
}