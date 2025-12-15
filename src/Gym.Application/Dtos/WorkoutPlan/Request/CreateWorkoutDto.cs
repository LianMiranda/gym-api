using System.ComponentModel.DataAnnotations;
using Gym.Domain.Enums;

namespace Gym.Application.Dtos.WorkoutPlan.Request;

public record CreateWorkoutDto
{
    [Required] public Guid UserId { get; init; }
    [Required] public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public sbyte? DaysPerWeek { get; init; }
    public sbyte? Months { get; init; }
    public Goal? Goal { get; init; }
}