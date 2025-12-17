using System.ComponentModel.DataAnnotations;

namespace Gym.Application.Dtos.Routine.Request;

public record CreateRoutineDto
{
    [Required] public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public string? ImageUrl { get; init; }
}