using Gym.Domain.Entities;

namespace Gym.Domain.Interfaces;

public interface IRoutineRepository
{
    void Create(Routine routine);
    void Update(Routine routine);
    void Delete(Routine routine);
    Task<IEnumerable<Routine>?> GetByWorkoutPlanIdAsync(Guid workoutPlanId, CancellationToken cancellationToken);
    Task<Routine?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}