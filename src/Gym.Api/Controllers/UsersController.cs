using Gym.Application.Dtos.User.Request;
using Gym.Application.UseCases.User;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly CreateUserUseCase _createUserUseCase;
    private readonly GetAllUsersUseCase _getAllUsersUseCase;
    private readonly DeleteUserUseCase _deleteUserUseCase;
    private readonly ILogger<UsersController> _logger;

    public UsersController(CreateUserUseCase createUserUseCase, GetAllUsersUseCase getAllUsersUseCase,
        DeleteUserUseCase deleteUserUseCase, ILogger<UsersController> logger)
    {
        _createUserUseCase = createUserUseCase;
        _getAllUsersUseCase = getAllUsersUseCase;
        _deleteUserUseCase = deleteUserUseCase;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
    {
        try
        {
            var user = await _createUserUseCase.ExecuteAsync(request);

            if (!user.IsSuccess)
                return Conflict(user);

            return Created("", user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error while creating user.");
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsersAsync([FromQuery] int page = 1, [FromQuery] int take = 20)
    {
        try
        {
            var user = await _getAllUsersUseCase.ExecuteAsync(page, take);

            if (!user.IsSuccess)
                return NotFound(user);

            return Ok(user);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error while searching for users.");
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id)
    {
        try
        {
            var result = await _deleteUserUseCase.ExecuteAsync(id);

            if (!result.IsSuccess)
                return NotFound(result);

            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Unexpected error while deleting user {UserId}", id);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }
}