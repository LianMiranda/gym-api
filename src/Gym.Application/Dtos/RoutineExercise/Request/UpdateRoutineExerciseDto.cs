namespace Gym.Application.Dtos.RoutineExercise.Request;

public record UpdateRoutineExerciseDto(
    int? OrderIndex,
    string? Notes
);