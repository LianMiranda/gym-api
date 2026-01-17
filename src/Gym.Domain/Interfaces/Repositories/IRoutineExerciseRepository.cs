using Gym.Domain.Entities;

namespace Gym.Domain.Interfaces.Repositories;

public interface IRoutineExerciseRepository
{
    void Create(RoutineExercise routineExercise);
    void Delete(RoutineExercise routineExercise);
    void Update(RoutineExercise routineExercise);
    Task<IEnumerable<RoutineExercise>> FindByRoutine(Guid routineId);
    Task<sbyte> GetMaxOrderIndexAsync(Guid routineId);
    Task<RoutineExercise?> GetByIdAsync(Guid id);
}