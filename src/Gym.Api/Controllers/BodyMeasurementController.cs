using Gym.Api.Shared;
using Gym.Application.Dtos.BodyMeasurement.Request;
using Gym.Application.Services.BodyMeasurement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Controllers;

[Authorize]
[Route("api/user/body")]
[ApiController]
public class BodyMeasurementController(
    ILogger<UsersController> logger,
    IBodyMeasurementService service,
    CurrentUserId currentUserId)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateBodyMeasurementAsync(CreateBodyMeasurementRequest request,
        CancellationToken cancellationToken = default)
    {
        var id = currentUserId.Get();

        if (id == Guid.Empty)
            return Unauthorized();
        try
        {
            request.UserId = id;

            var user = await service.CreateAsync(request, cancellationToken);

            if (!user.IsSuccess)
                return NotFound(user);

            return Ok(user);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error while searching for body measurements.");
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetBodyMeasurementByUserIdAsync([FromQuery] int page = 1,
        [FromQuery] int take = 20,
        CancellationToken cancellationToken = default)
    {
        var id = currentUserId.Get();

        if (id == Guid.Empty)
            return Unauthorized();
        try
        {
            var user = await service.GetByUserIdAsync(id, page, take, cancellationToken);

            if (!user.IsSuccess)
                return NotFound(user);

            return Ok(user);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error while searching for body measurements.");
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> GetBodyMeasurementByIdAsync([FromRoute] Guid id,
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
            logger.LogError(e, "Unexpected error while searching for user {UserId}", id);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteBodyMeasurementAsync([FromRoute] Guid id,
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
    public async Task<IActionResult> UpdateBodyMeasurementAsync([FromRoute] Guid id,
        [FromBody] UpdateBodyMeasurementRequest request,
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
}