using System.Security.Claims;
using Gym.Application.Dtos.User.Request;
using Gym.Application.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Controllers;

[Authorize]
[Route("api/users")]
[ApiController]
public class UsersController(ILogger<UsersController> logger, IUserService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync([FromQuery] int page = 1, [FromQuery] int take = 20,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await service.GetAllAsync(page, take, cancellationToken);

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
    [HttpGet]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid id,
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

    [HttpDelete]
    public async Task<IActionResult> DeleteCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        var id  = GetCurrentId();
        
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

    [HttpPatch]
    public async Task<IActionResult> UpdateCurrentUserAsync([FromBody] UpdateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        var id  = GetCurrentId();
        
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