namespace Gym.Application.Dtos.Routine.Request;

public record UpdateRoutineDto
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public string? ImageUrl { get; init; }
    public sbyte? OrderIndex { get; init; }
};