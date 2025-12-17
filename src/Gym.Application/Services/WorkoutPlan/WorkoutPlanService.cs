using Gym.Application.Dtos.WorkoutPlan.Request;
using Gym.Application.Dtos.WorkoutPlan.Response;
using Gym.Application.MappingImplementation;
using Gym.Domain.Abstractions.ResultPattern;
using Gym.Domain.Interfaces.Repositories;
using Gym.Domain.Interfaces.UnitOfWork;

namespace Gym.Application.Services.WorkoutPlan;

public class WorkoutPlanService(
    IWorkoutPlanRepository workoutPlanRepository,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository) : IWorkoutPlanService
{
    private readonly IWorkoutPlanRepository _workoutPlanRepository = workoutPlanRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result<WorkoutPlanResponseDto>> CreateAsync(CreateWorkoutDto request,
        CancellationToken cancellationToken)
    {
        var entity = request.ToEntity();

        await _workoutPlanRepository.CreateAsync(entity, cancellationToken);
        await _unitOfWork.SaveAsync();

        var response = entity.ToDto();
        return response.ToSuccessResult();
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateWorkoutDto request, CancellationToken cancellationToken)
    {
        var result = await _workoutPlanRepository.GetByIdAsync(id, cancellationToken);

        if (result == null)
            return Result.Error("Workout Plan not found");

        if (!string.IsNullOrWhiteSpace(request.Name)) result.UpdateName(request.Name);
        if (!string.IsNullOrWhiteSpace(request.Description)) result.UpdateDescription(request.Description);
        if (request.DaysPerWeek.HasValue) result.UpdateDaysPerWeek(request.DaysPerWeek.Value);
        if (request.Months.HasValue) result.UpdateMonths(request.Months.Value);
        if (request.Goal.HasValue) result.UpdateGoal(request.Goal.Value);

        _workoutPlanRepository.Update(result);
        await _unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var workoutPlanExists = await _workoutPlanRepository.GetByIdAsync(id, cancellationToken);

        if (workoutPlanExists == null)
            return Result.Error("Workout Plan not found");

        _workoutPlanRepository.Delete(workoutPlanExists);
        await _unitOfWork.SaveAsync();

        return Result.Success();
    }

    public async Task<Result<IEnumerable<WorkoutPlanResponseDto>?>> GetByUserIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.GetByIdAsync(id);

        if (userExists == null)
            return Result<IEnumerable<WorkoutPlanResponseDto>?>.Error("User not found");

        var list = await _workoutPlanRepository.GetByUserIdAsync(id, cancellationToken);

        if (list == null || list.Count() == 0)
            return Result<IEnumerable<WorkoutPlanResponseDto>?>.Error("Workouts Plan not found");

        var dto = list.Select(workoutPlan => workoutPlan.ToDto());

        return dto.ToSuccessResult()!;
    }
}