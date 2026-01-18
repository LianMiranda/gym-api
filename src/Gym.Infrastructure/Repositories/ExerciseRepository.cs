using Gym.Domain.Entities;
using Gym.Domain.Enums.Exercise_Enums;
using Gym.Domain.Interfaces.Repositories;
using Gym.Infrastructure.Database.Context;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Repositories;

public class ExerciseRepository(AppDbContext context) : IExerciseRepository
{
    public async Task<List<Exercise>?> GetExercisesByMuscleAsync(MuscleGroup muscleGroup)
    {
        return await context.Exercises
            .AsNoTracking()
            .Where(e => e.MuscleGroup == muscleGroup)
            .ToListAsync();
    }

    public async Task<(IEnumerable<Exercise>? exercises, int totalCount, int page, int pageSize)> GetAllAsync(int page,
        int take)
    {
        if (page <= 0) page = 1;
        if (take <= 0) take = 20;

        take = Math.Min(take, 50);

        var query = context.Exercises.AsNoTracking();
        var totalCount = await query.CountAsync();
        var skip = (page - 1) * take;
        var exercises = await query.Skip(skip).Take(take).ToListAsync();

        return (exercises, totalCount, page, take);
    }
}