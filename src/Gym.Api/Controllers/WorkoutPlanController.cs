using System.Security.Claims;
using Gym.Application.Dtos.User.Request;
using Gym.Application.Dtos.WorkoutPlan.Request;
using Gym.Application.Services.User;
using Gym.Application.Services.WorkoutPlan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Controllers;

[Authorize]
[Route("api/workout")]
[ApiController]
public class WorkoutPlanController(ILogger<WorkoutPlanController> logger, IWorkoutPlanService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateWorkoutDto request,
        CancellationToken cancellationToken = default)
    {
        var id = GetCurrentId();

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
            logger.LogError(e, "Unexpected error while creating user.");
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetByUserIdAsync(CancellationToken cancellationToken = default)
    {
        var id = GetCurrentId();

        if (id == Guid.Empty)
            return Unauthorized();

        try
        {
            var user = await service.GetByUserIdAsync(id, cancellationToken);

            if (!user.IsSuccess)
                return NotFound(user);

            return Ok(user);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error while searching for users.");
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteWorkoutPlanAsync([FromRoute] Guid id,
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
            logger.LogError(e, "Unexpected error while deleting user {UserId}", id);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{id}")]
    [HttpPatch]
    public async Task<IActionResult> UpdateWorkoutPlanAsync([FromRoute] Guid id, [FromBody] UpdateWorkoutDto request,
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
            logger.LogError(e, "Unexpected error while update user {UserId}", id);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    private Guid GetCurrentId()
    {
        var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(claim?.Value, out var id))
        {
            logger.LogWarning("Invalid user ID format in token: {ClaimValue}", claim?.Value);
        }

        return id;
    }
}