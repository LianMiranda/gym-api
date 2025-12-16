using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Gym.Domain.Enums;

namespace Gym.Application.Dtos.WorkoutPlan.Request;

public record CreateWorkoutDto
{
    [Required] [JsonIgnore] public Guid UserId { get; set; }
    [Required] public string Name { get; init; } = null!;
    public string? Description { get; init; }

    [JsonPropertyName("days_per_week")]
    [Range(1, 7, ErrorMessage = "{0} must be between {1} and {2}")]
    public sbyte? DaysPerWeek { get; init; }

    [Range(1, 12, ErrorMessage = "{0} must be between {1} and {2}")]
    public sbyte? Months { get; init; }

    public Goal? Goal { get; init; }
}