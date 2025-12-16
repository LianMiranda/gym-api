using Gym.Domain.Entities;
using Gym.Domain.Interfaces.Repositories;
using Gym.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Repositories;

public class WorkoutPlanRepository(AppDbContext context) : IWorkoutPlanRepository
{
    private readonly AppDbContext _context = context;

    public async Task CreateAsync(WorkoutPlan workoutPlan, CancellationToken cancellationToken)
    {
        await _context.WorkoutPlans.AddAsync(workoutPlan, cancellationToken);
    }

    public void Update(WorkoutPlan workoutPlan)
    {
        _context.Entry(workoutPlan).State = EntityState.Modified;
    }

    public void Delete(WorkoutPlan workoutPlan)
    {
        _context.WorkoutPlans.Remove(workoutPlan);
    }

    public async Task<WorkoutPlan?> GetByIdAsync(Guid workoutPlanId)
    {
        return await _context.WorkoutPlans
            .AsNoTracking()
            .SingleOrDefaultAsync(w => w.Id == workoutPlanId);
    }

    public async Task<IEnumerable<WorkoutPlan>?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _context.WorkoutPlans
            .AsNoTracking()
            .Where(w => w.UserId == userId)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}