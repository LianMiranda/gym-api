using Gym.Domain.Entities;
using Gym.Domain.Interfaces.Repositories;
using Gym.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Repositories;

public class BodyMeasurementRepository : IBodyMeasurementRepository
{
    private readonly AppDbContext _context;

    public BodyMeasurementRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(BodyMeasurement bodyMeasurement)
    {
        await _context.BodyMeasurements.AddAsync(bodyMeasurement);
    }

    public void Delete(BodyMeasurement bodyMeasurement)
    {
        _context.BodyMeasurements.Remove(bodyMeasurement);
    }

    public void Update(BodyMeasurement bodyMeasurement)
    {
        _context.Entry<BodyMeasurement>(bodyMeasurement).State = EntityState.Modified;
    }

    public async Task<BodyMeasurement?> GetByIdAsync(Guid id)
    {
        return await _context.BodyMeasurements.AsNoTracking()
            .SingleOrDefaultAsync(bodyMeasurement => bodyMeasurement.Id == id);
    }

    public async Task<(IEnumerable<BodyMeasurement>? bodyMeasurements, int totalCount, int page, int pageSize)>
        GetByUserIdAsync(Guid userId, int page, int take)
    {
        if (page <= 0) page = 1;
        if (take <= 0) take = 10;

        take = Math.Min(take, 50);

        var query = _context.BodyMeasurements.AsNoTracking();
        var totalCount = await query.CountAsync();
        var skip = (page - 1) * take;
        var bodyMeasurements = await query.Where(bm => bm.UserId == userId).Skip(skip).Take(take).ToListAsync();

        return (bodyMeasurements, totalCount, page, take);
    }
}