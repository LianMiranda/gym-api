using Gym.Application.Dtos.RoutineExercise.Request;
using Gym.Application.Services.RoutineExercise;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Controllers;

[Authorize]
[Route("api/routine/exercise")]
[ApiController]
public class RoutineExerciseController(
    ILogger<RoutineController> logger,
    IRoutineExerciseService service) : ControllerBase
{
    [Route("{routineId}/{exerciseId}")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromRoute] Guid routineId,
        [FromRoute] Guid exerciseId,
        [FromBody] CreateRoutineExerciseDto request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await service.CreateAsync(request, routineId, exerciseId, cancellationToken);

            return Created("", result);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Unexpected error while creating routine exercise for routine with id {routineId}.",
                routineId);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{routineId}")]
    [HttpGet]
    public async Task<IActionResult> GetByRoutineIdAsync(
        [FromRoute] Guid routineId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await service.GetByRoutineIdAsync(routineId, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Unexpected error while retrieving exercises for routine with id {routineId}.",
                routineId);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await service.DeleteAsync(id, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result);

            return NoContent();
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Unexpected error while deleting exercise with Id {id}.",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{id}")]
    [HttpPatch]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateRoutineExerciseDto request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await service.UpdateAsync(id, request, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result);

            return NoContent();
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Unexpected error while updating exercise with Id {id}.",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }
}