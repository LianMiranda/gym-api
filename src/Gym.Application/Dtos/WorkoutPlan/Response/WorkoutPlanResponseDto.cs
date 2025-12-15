using Gym.Domain.Enums;

namespace Gym.Application.Dtos.WorkoutPlan.Response;

public record WorkoutPlanResponseDto
{
    public string Name { get; init; } = null!;
    public string? Description { get; init; }
    public sbyte? DaysPerWeek { get; init; }
    public sbyte? Months { get; init; }
    public Goal? Goal { get; init; }
}