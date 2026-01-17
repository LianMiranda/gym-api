using Gym.Application.Dtos.RoutineExercise.Request;
using Gym.Application.Dtos.RoutineExercise.Response;
using Gym.Domain.Abstractions.ResultPattern;

namespace Gym.Application.Services.RoutineExercise;

public interface IRoutineExerciseService
{
    Task<Result> CreateAsync(CreateRoutineExerciseDto createRoutineDto, Guid routineId, Guid exerciseId,
        CancellationToken cancellationToken);

    Task<Result> DeleteAsync(Guid routineExerciseId, CancellationToken cancellationToken);

    Task<Result> UpdateAsync(Guid routineExerciseId, UpdateRoutineExerciseDto request,
        CancellationToken cancellationToken);

    Task<Result<IEnumerable<RoutineExerciseResponseDto>?>> GetByRoutineIdAsync(Guid routineId,
        CancellationToken cancellationToken);
}