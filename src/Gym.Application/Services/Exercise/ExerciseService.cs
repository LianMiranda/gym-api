using Gym.Application.Dtos.Exercise.Response;
using Gym.Application.MappingImplementation;
using Gym.Domain.Abstractions.ResultPattern;
using Gym.Domain.Enums.Exercise_Enums;
using Gym.Domain.Interfaces.Repositories;

namespace Gym.Application.Services.Exercise;

public class ExerciseService(IExerciseRepository repository) : IExerciseService
{
    public async Task<Result<IEnumerable<ExerciseResponseDto>?>> GetExercisesByMuscleAsync(MuscleGroup muscleGroup)
    {
        var result = await repository.GetExercisesByMuscleAsync(muscleGroup);

        if (result == null || result.Count == 0)
            return Result<IEnumerable<ExerciseResponseDto>?>.Error("No exercise found");

        var response = result.Select(e => e.ToDto());

        return response.ToSuccessResult()!;
    }

    public async Task<Result<PagedExerciseResponseDto>> GetAllAsync(int take, int page)
    {
        var result = await repository.GetAllAsync(page, take);

        var currentPage = result.page;
        var totalCount = result.totalCount;
        var pageSize = result.pageSize;


        if (result.exercises == null || !result.exercises.Any())
            return Result<PagedExerciseResponseDto>.Error(
                page == 1
                    ? "No exercises found"
                    : "No exercises found for the requested page"
            );

        var viewExercises = result.exercises.Select(e => e.ToDto()).ToList();

        var response = new PagedExerciseResponseDto()
        {
            Response = viewExercises!,
            CurrentPage = currentPage,
            TotalCount = totalCount,
            PageSize = pageSize
        };

        return response.ToSuccessResult();
    }
}