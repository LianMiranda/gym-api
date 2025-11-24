namespace Gym.Application.Dtos.User.Response;

public record PagedUsersResponse
{
    public List<UserResponse>? Users { get; set; } 
    public int CurrentPage { get; set; }
    public int TotalCount { get; set; }
    public int PageSize { get; set; }
}