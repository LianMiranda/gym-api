using Gym.Domain.Entities;
using Gym.Domain.Interfaces.Repositories;
using Gym.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Repositories;

public class RoutineExerciseRepository(AppDbContext context) : IRoutineExerciseRepository
{
    public void Create(RoutineExercise routineExercise)
    {
        context.Add(routineExercise);
    }

    public void Delete(RoutineExercise routineExercise)
    {
        context.Remove(routineExercise);
    }

    public void Update(RoutineExercise routineExercise)
    {
        context.Entry<RoutineExercise>(routineExercise).State = EntityState.Modified;
    }

    public async Task<IEnumerable<RoutineExercise>> FindByRoutine(Guid routineId)
    {
        return await context.RoutineExercises.AsNoTracking()
            .Where(re => re.RoutineId == routineId)
            .OrderBy(re => re.OrderIndex)
            .Include(re => re.Sets)
            .Include(re => re.Exercise)
            .ToListAsync();
    }
    
    public async Task<RoutineExercise?> GetByIdAsync(Guid id)
    {
        return await context.RoutineExercises.AsNoTracking()
            .FirstOrDefaultAsync(re => re.Id == id);
    }
    
    public async Task<sbyte> GetMaxOrderIndexAsync(Guid routineId)
    {
        return await context.RoutineExercises
            .Where(routine => routine.RoutineId == routineId)
            .Select(r => (sbyte?)r.OrderIndex)
            .MaxAsync() ?? -1;
    }
}