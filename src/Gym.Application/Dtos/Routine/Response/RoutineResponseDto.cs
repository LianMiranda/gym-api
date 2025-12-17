namespace Gym.Application.Dtos.Routine.Response;

public record RoutineResponseDto(
    Guid Id,
    string Name,
    string? Description,
    string? ImageUrl,
    sbyte OrderIndex
);