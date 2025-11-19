using Gym.Application.Dtos.User;
using Gym.Application.UseCases.User;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private CreateUserUseCase _createUserUseCase;

    public UsersController(CreateUserUseCase createUserUseCase)
    {
        _createUserUseCase = createUserUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserRequest request)
    {
        try
        {
            var user = await _createUserUseCase.ExecuteAsync(request);

            if (!user.IsSuccess)
                return Conflict(user);

            return Ok(user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}