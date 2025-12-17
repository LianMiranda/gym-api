namespace Gym.Application.Dtos.Routine.Response;

public record RoutineResponseDto(
    string? Name,
    string? Description,
    string? ImageUrl,
    sbyte? OrderIndex
);