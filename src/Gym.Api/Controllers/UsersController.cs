using Gym.Application.Dtos.User.Request;
using Gym.Application.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController(ILogger<UsersController> logger, IUserService service) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var user = await service.CreateAsync(request, cancellationToken);

            if (!user.IsSuccess)
                return Conflict(user);

            return Created("", user);
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

    [Route("{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
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
    public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, [FromBody] UpdateUserRequest request,
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