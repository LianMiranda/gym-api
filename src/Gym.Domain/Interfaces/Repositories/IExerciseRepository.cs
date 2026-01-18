using Gym.Domain.Entities;
using Gym.Domain.Enums.Exercise_Enums;

namespace Gym.Domain.Interfaces.Repositories;

public interface IExerciseRepository
{
    Task<List<Exercise>?> GetExercisesByMuscleAsync(MuscleGroup muscleGroup);

    Task<(IEnumerable<Exercise>? exercises, int totalCount, int page, int pageSize)> GetAllAsync(int page,
        int take);
}