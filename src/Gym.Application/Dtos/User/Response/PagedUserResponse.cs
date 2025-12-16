using Gym.Application.Dtos.Base;

namespace Gym.Application.Dtos.User.Response;

public record PagedUsersResponse : PagedResponse<UserResponse?>
{
}