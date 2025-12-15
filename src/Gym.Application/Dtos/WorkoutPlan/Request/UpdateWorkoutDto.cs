using Gym.Domain.Enums;

namespace Gym.Application.Dtos.WorkoutPlan.Request;

public record UpdateWorkoutDto
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public sbyte? DaysPerWeek { get; init; }
    public sbyte? Months { get; init; }
    public Goal? Goal { get; init; }
}