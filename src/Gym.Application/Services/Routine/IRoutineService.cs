using Gym.Application.Dtos.Routine.Request;
using Gym.Application.Dtos.Routine.Response;
using Gym.Domain.Abstractions.ResultPattern;

namespace Gym.Application.Services.Routine;

public interface IRoutineService
{
    Task<Result<RoutineResponseDto>> CreateAsync(Guid workoutPlanId, CreateRoutineDto createRoutineDto, CancellationToken cancellationToken);
    Task<Result> DeleteAsync(Guid routineId, CancellationToken cancellationToken);
    Task<Result> UpdateAsync(Guid routineId, UpdateRoutineDto request, CancellationToken cancellationToken);
    Task<Result<IEnumerable<RoutineResponseDto>?>> GetByWorkoutPlanIdAsync(Guid workoutPlanId,  CancellationToken cancellationToken);
    Task<Result<RoutineResponseDto?>> GetByIdAsync(Guid routineId, CancellationToken cancellationToken);
}