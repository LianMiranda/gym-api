using Gym.Application.Dtos.Exercise.Response;
using Gym.Domain.Abstractions.ResultPattern;
using Gym.Domain.Enums.Exercise_Enums;

namespace Gym.Application.Services.Exercise;

public interface IExerciseService
{
    Task<Result<IEnumerable<ExerciseResponseDto>?>> GetExercisesByMuscleAsync(MuscleGroup muscleGroup);
    Task<Result<PagedExerciseResponseDto>> GetAllAsync(int take, int page);
}