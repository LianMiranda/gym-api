using System.Security.Claims;

namespace Gym.Api.Shared;

public class CurrentUserId(IHttpContextAccessor accessor)
{
    public Guid Get()
    {
        var userId = accessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userId?.Value, out var id))
            throw new InvalidOperationException("Invalid user id");

        return id;
    }
}