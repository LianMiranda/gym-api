using Gym.Application.Dtos.RoutineExercise.Request;
using Gym.Application.Dtos.RoutineExercise.Response;
using Gym.Application.MappingImplementation;
using Gym.Domain.Abstractions.ResultPattern;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.UnitOfWork;

namespace Gym.Application.Services.RoutineExercise;

public class RoutineExerciseService(
    IRoutineExerciseRepository repository,
    IUnitOfWork unitOfWork,
    IRoutineRepository routineRepository) : IRoutineExerciseService
{
    public async Task<Result> CreateAsync(CreateRoutineExerciseDto createRoutineDto, Guid routineId, Guid exerciseId,
        CancellationToken cancellationToken)
    {
        var maxOrderIndex = await repository.GetMaxOrderIndexAsync(routineId);

        var entity = createRoutineDto.ToEntity(routineId, exerciseId,
            (sbyte)(maxOrderIndex + 1));

        repository.Create(entity);
        await unitOfWork.SaveAsync();

        var response = entity.ToDto();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var routine = await repository.GetByIdAsync(id);

        if (routine == null)
            return Result.Error("Routine Exercise not found");

        repository.Delete(routine);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result<IEnumerable<RoutineExerciseResponseDto>?>> GetByRoutineIdAsync(Guid routineId,
        CancellationToken cancellationToken)
    {
        var routine = routineRepository.GetByIdAsync(routineId, cancellationToken);

        if (routine == null)
            return Result<IEnumerable<RoutineExerciseResponseDto>?>.Error("Routine not found");

        var exercises = await repository.FindByRoutine(routineId);

        if (exercises.Count() == 0)
            return Result<IEnumerable<RoutineExerciseResponseDto>?>.Error("Routine Exercises not found");

        var response = exercises.Select(r => r.ToDto());

        return response.ToSuccessResult()!;
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateRoutineExerciseDto request,
        CancellationToken cancellationToken)
    {
        var result = await repository.GetByIdAsync(id);

        if (result == null)
            return Result.Error("Routine Exercise not found");

        if (request.OrderIndex.HasValue && request.OrderIndex >= 0) result.UpdateOrderIndex(request.OrderIndex.Value);
        if (!string.IsNullOrWhiteSpace(request.Notes)) result.UpdateNotes(request.Notes);

        repository.Update(result);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }
}