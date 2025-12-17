using Gym.Application.Dtos.Routine.Request;
using Gym.Application.Dtos.Routine.Response;
using Gym.Application.MappingImplementation;
using Gym.Domain.Abstractions.ResultPattern;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.UnitOfWork;

namespace Gym.Application.Services.Routine;

public class RoutineService(
    IRoutineRepository repository,
    IUnitOfWork unitOfWork,
    IWorkoutPlanRepository workoutPlanRepository) : IRoutineService
{
    public async Task<Result<RoutineResponseDto>> CreateAsync(Guid workoutPlanId, CreateRoutineDto createRoutineDto,
        CancellationToken cancellationToken)
    {
        var maxOrderIndex = await repository.GetMaxOrderIndexAsync(workoutPlanId, cancellationToken);
        
        var entity = createRoutineDto.ToEntity(workoutPlanId, (sbyte)(maxOrderIndex + 1));

        repository.Create(entity);
        await unitOfWork.SaveAsync();

        var response = entity.ToDto();
        return response.ToSuccessResult();
    }

    public async Task<Result> DeleteAsync(Guid routineId, CancellationToken cancellationToken)
    {
        var routine = await repository.GetByIdAsync(routineId, cancellationToken);

        if (routine == null)
            return Result.Error("Routine not found");

        repository.Delete(routine);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateAsync(Guid routineId, UpdateRoutineDto request,
        CancellationToken cancellationToken)
    {
        var result = await repository.GetByIdAsync(routineId, cancellationToken);

        if (result == null)
            return Result.Error("Workout Plan not found");

        if (!string.IsNullOrWhiteSpace(request.Name)) result.UpdateName(request.Name);
        if (!string.IsNullOrWhiteSpace(request.Description)) result.UpdateDescription(request.Description);
        if (!string.IsNullOrWhiteSpace(request.ImageUrl)) result.UpdateImageUrl(request.ImageUrl);
        if (request.OrderIndex > 0) result.UpdateOrderIndex(request.OrderIndex.Value);

        repository.Update(result);
        await unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result<IEnumerable<RoutineResponseDto>?>> GetByWorkoutPlanIdAsync(Guid workoutPlanId,
        CancellationToken cancellationToken)
    {
        var workoutPlan = await workoutPlanRepository.GetByIdAsync(workoutPlanId, cancellationToken);

        if (workoutPlan == null)
            return Result<IEnumerable<RoutineResponseDto>?>.Error("Workout plan not found");

        var routines = await repository.GetByWorkoutPlanIdAsync(workoutPlanId, cancellationToken);

        if (routines == null || routines.Count() == 0)
            return Result<IEnumerable<RoutineResponseDto>?>.Error("Routines not found");

        var response = routines.Select(r => r.ToDto());

        return response.ToSuccessResult()!;
    }

    public async Task<Result<RoutineResponseDto?>> GetByIdAsync(Guid routineId, CancellationToken cancellationToken)
    {
        var routine = await repository.GetByIdAsync(routineId, cancellationToken);

        if (routine == null)
            return Result<RoutineResponseDto?>.Error("Routine not found");

        var response = routine.ToDto();

        return response.ToSuccessResult()!;
    }
}