using Gym.Application.Dtos.Exercise.Response;
using Gym.Domain.Entities;

namespace Gym.Application.Dtos.RoutineExercise.Response;

public record RoutineExerciseResponseDto(
    Guid Id,
    int OrderIndex,
    ExerciseInRoutineDto Exercise,
    string Notes,
    List<ExerciseSet> Sets
);