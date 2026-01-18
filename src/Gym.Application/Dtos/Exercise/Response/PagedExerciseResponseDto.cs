using Gym.Application.Dtos.Base;

namespace Gym.Application.Dtos.Exercise.Response;

public record PagedExerciseResponseDto : PagedResponse<ExerciseResponseDto?>
{}