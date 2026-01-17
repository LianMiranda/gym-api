using Gym.Api.Shared;
using Gym.Application.Dtos.WorkoutPlan.Request;
using Gym.Application.Services.WorkoutPlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Controllers;

[Authorize]
[Route("api/workout")]
[ApiController]
public class WorkoutPlanController(
    ILogger<WorkoutPlanController> logger,
    IWorkoutPlanService service,
    CurrentUserId currentUserId) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(
        CreateWorkoutDto request,
        CancellationToken cancellationToken = default)
    {
        var id = currentUserId.Get();

        if (id == Guid.Empty)
            return Unauthorized();

        try
        {
            request.UserId = id;
            var result = await service.CreateAsync(request, cancellationToken);

            return Created("", result);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Unexpected error while creating workout plan for UserId {UserId}.",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetByUserIdAsync(
        CancellationToken cancellationToken = default)
    {
        var id = currentUserId.Get();

        if (id == Guid.Empty)
            return Unauthorized();

        try
        {
            var result = await service.GetByUserIdAsync(id, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Unexpected error while retrieving workout plans for UserId {UserId}.",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteWorkoutPlanAsync(
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
                "Unexpected error while deleting workout plan with Id {WorkoutPlanId}.",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{id}")]
    [HttpPatch]
    public async Task<IActionResult> UpdateWorkoutPlanAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateWorkoutDto request,
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
                "Unexpected error while updating workout plan with Id {WorkoutPlanId}.",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }
}