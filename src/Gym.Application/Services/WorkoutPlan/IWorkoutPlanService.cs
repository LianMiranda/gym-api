using Gym.Application.Dtos.WorkoutPlan.Request;
using Gym.Application.Dtos.WorkoutPlan.Response;
using Gym.Domain.Abstractions.ResultPattern;

namespace Gym.Application.Services.WorkoutPlan;

public interface IWorkoutPlanService
{
    Task<Result<WorkoutPlanResponseDto>> CreateAsync(CreateWorkoutDto request, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(Guid id, UpdateWorkoutDto request, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<IEnumerable<WorkoutPlanResponseDto>?>> GetByUserIdAsync(Guid id, CancellationToken cancellationToken);
}