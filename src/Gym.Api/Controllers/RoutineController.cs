using Gym.Application.Dtos.Routine.Request;
using Gym.Application.Services.Routine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Controllers;

[Authorize]
[Route("api/routine")]
[ApiController]
public class RoutineController(
    ILogger<RoutineController> logger,
    IRoutineService service) : ControllerBase
{
    [Route("{workoutPlanId}")]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        [FromRoute] Guid workoutPlanId,
        [FromBody] CreateRoutineDto request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await service.CreateAsync(workoutPlanId, request, cancellationToken);

            return Created("", result);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Unexpected error while creating routine for WorkoutPlanId {WorkoutPlanId}.",
                workoutPlanId);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("workout/{workoutPlanId}")]
    [HttpGet]
    public async Task<IActionResult> GetByWorkoutPlanIdAsync(
        [FromRoute] Guid workoutPlanId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await service.GetByWorkoutPlanIdAsync(workoutPlanId, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Unexpected error while retrieving routines for WorkoutPlanId {WorkoutPlanId}.",
                workoutPlanId);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await service.GetByIdAsync(id, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Unexpected error while retrieving routine with Id {RoutineId}.",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteRoutineAsync(
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
                "Unexpected error while deleting routine with Id {RoutineId}.",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{id}")]
    [HttpPatch]
    public async Task<IActionResult> UpdateRoutineAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateRoutineDto request,
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
                "Unexpected error while updating routine with Id {RoutineId}.",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }
}