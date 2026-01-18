using Gym.Application.Services.Exercise;
using Gym.Domain.Enums.Exercise_Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Controllers;

[Authorize]
[Route("api/exercises")]
[ApiController]
public class ExerciseController(
    ILogger<ExerciseController> logger,
    IExerciseService service) : ControllerBase
{
    [Route("muscle")]
    [HttpGet]
    public async Task<IActionResult> GetByCategoryAsync(
        [FromQuery] MuscleGroup muscle,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await service.GetExercisesByMuscleAsync(muscle);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Unexpected error while retrieving exercises for muscle {muscleGroup}.",
                muscle);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(
        [FromQuery] int page = 1,
        [FromQuery] int take = 20,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await service.GetAllAsync(take, page);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Unexpected error while retrieving exercises. Page {Page}, Take {Take}.",
                page,
                take);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }
}