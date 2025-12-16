using Gym.Domain.Entities;
using Gym.Domain.Interfaces;
using Gym.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Repositories;

public class RoutineRepository(AppDbContext context) : IRoutineRepository
{
    public void Create(Routine routine)
    {
        context.Routines.Add(routine);
    }

    public void Update(Routine routine)
    {
        context.Entry(routine).State = EntityState.Modified;
    }

    public void Delete(Routine routine)
    {
        context.Routines.Remove(routine);
    }

    public async Task<IEnumerable<Routine>?> GetByWorkoutPlanIdAsync(Guid workoutPlanId,
        CancellationToken cancellationToken)
    {
        //TODO: Criar index para "Workout Plan ID"
        return await context.Routines
            .AsNoTracking()
            .Where(routine => routine.WorkoutPlanId == workoutPlanId)
            .OrderBy(routine => routine.OrderIndex)
            .ToListAsync(cancellationToken);
    }

    public async Task<Routine?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await context.Routines
            .FirstOrDefaultAsync(routine => routine.Id == id, cancellationToken);
    }
}