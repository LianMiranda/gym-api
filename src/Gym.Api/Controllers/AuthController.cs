using System.Security.Claims;
using Gym.Application.Dtos.Auth.Request;
using Gym.Application.Dtos.User.Request;
using Gym.Application.Services.Auth;
using Gym.Application.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ILogger<AuthController> logger, IAuthService authService) : ControllerBase
{
    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await authService.LoginAsync(request, cancellationToken);

            if (!result.IsSuccess)
                return Unauthorized(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error while logging in user.");
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }

    [Route("register")]
    [HttpPost]
    public async Task<IActionResult> RegisterAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await authService.RegisterAsync(request, cancellationToken);

            if (!result.IsSuccess)
                return Conflict(result);

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

    [Authorize]
    [Route("me")]
    [HttpGet]
    public async Task<IActionResult> GetCurrentUserAsync([FromServices] IUserService userService,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var claim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (claim == null)
                return Unauthorized();

            if (!Guid.TryParse(claim.Value, out var id))
            {
                logger.LogWarning("Invalid user ID format in token: {ClaimValue}", claim.Value);
                return Unauthorized();
            }

            var result = await userService.GetByIdAsync(id, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Unexpected error while searching for user");
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "An unexpected error occurred" }
            );
        }
    }
}