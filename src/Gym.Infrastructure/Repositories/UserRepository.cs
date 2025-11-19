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

    public async Task<(IEnumerable<User>? users, int totalCount, int page, int pageSize)> GetAllAsync(int page,
        int take)
    {
        if (page <= 0) page = 1;
        if (take <= 0) take = 10;

        take = Math.Min(take, 50);

        var query = _context.Users.AsNoTracking();
        var totalCount = await query.CountAsync();
        var skip = (page - 1) * take;
        var users = await query.Skip(skip).Take(take).ToListAsync();

        return (users, totalCount, page, take);
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        var user = await _context.Users.AsNoTracking().SingleOrDefaultAsync(user => user.Email == email);

        if (user == null)
            return false;

        return true;
    }
}