using Gym.Api.Shared;
using Gym.Application.Dtos.User.Request;
using Gym.Application.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Controllers;

[Authorize]
[Route("api/users")]
[ApiController]
public class UsersController(
    ILogger<UsersController> logger,
    IUserService service,
    CurrentUserId currentUserId)
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync(
        [FromQuery] int page = 1,
        [FromQuery] int take = 20,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var result = await service.GetAllAsync(page, take, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Unexpected error while retrieving users. Page {Page}, Take {Take}.",
                page,
                take);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{id}")]
    [HttpGet]
    public async Task<IActionResult> GetUserByIdAsync(
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
                "Unexpected error while retrieving user with Id {UserId}.",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("me")]
    [HttpDelete]
    public async Task<IActionResult> DeleteCurrentUserAsync(
        CancellationToken cancellationToken = default)
    {
        var id = currentUserId.Get();

        if (id == Guid.Empty)
            return Unauthorized();

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
                "Unexpected error while deleting user with Id {UserId}.",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("me")]
    [HttpPatch]
    public async Task<IActionResult> UpdateCurrentUserAsync(
        [FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var id = currentUserId.Get();

        if (id == Guid.Empty)
            return Unauthorized();

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
                "Unexpected error while updating user with Id {UserId}.",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }
}