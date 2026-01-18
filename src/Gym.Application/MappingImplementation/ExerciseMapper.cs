using Gym.Application.Dtos.Exercise.Response;
using Gym.Domain.Entities;

namespace Gym.Application.MappingImplementation;

public static class ExerciseMapper
{
    public static ExerciseResponseDto ToDto(this Exercise routine)
    {
        return new ExerciseResponseDto(
            routine.Id,
            routine.Name,
            routine.Description,
            routine.Category,
            routine.MuscleGroup,
            routine.Equipment,
            routine.DifficultyLevel
        );
    }
}