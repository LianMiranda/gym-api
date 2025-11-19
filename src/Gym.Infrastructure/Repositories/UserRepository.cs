using Gym.Domain.Entities;
using Gym.Domain.Interfaces.Repositories;
using Gym.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public void Delete(User user)
    {
        _context.Users.Remove(user);
    }

    public void Update(User user)
    {
        _context.Entry<User>(user).State = EntityState.Modified;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users.AsNoTracking().SingleOrDefaultAsync(user => user.Id == id);
    }

    public async Task<IEnumerable<User>?> GetAllAsync()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(user => user.Email == email);

        if (user == null)
            return false;

        return true;
    }
}