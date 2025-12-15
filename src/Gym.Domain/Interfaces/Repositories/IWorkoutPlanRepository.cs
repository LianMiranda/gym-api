using Gym.Domain.Entities;

namespace Gym.Domain.Interfaces.Repositories;

public interface IWorkoutPlanRepository
{
    Task CreateAsync(WorkoutPlan workoutPlan, CancellationToken cancellationToken);
    void Update(WorkoutPlan workoutPlan);
    void Delete(WorkoutPlan workoutPlan);
    Task<WorkoutPlan?> GetById(Guid workoutPlanId);
    Task<IEnumerable<WorkoutPlan>?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
}