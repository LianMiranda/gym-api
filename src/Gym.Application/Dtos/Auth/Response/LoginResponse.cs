namespace Gym.Application.Dtos.Auth.Response;

public record LoginResponse
{
    public string? Token { get; set; }
}