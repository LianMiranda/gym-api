using Gym.Domain.Enums.Exercise_Enums;

namespace Gym.Application.Dtos.Exercise.Response;

public record ExerciseResponseDto(
    Guid Id,
    string Name,
    string Description,
    ExerciseCategory Category, 
    MuscleGroup MuscleGroup,
    Equipment Equipment,
    DifficultyLevel Difficulty
);