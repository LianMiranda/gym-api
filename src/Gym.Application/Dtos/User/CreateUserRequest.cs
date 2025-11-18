using System.Text.Json.Serialization;

namespace Gym.Application.Dtos.User;

public record CreateUserRequest
{
    [JsonPropertyName("first_name")] public string FirstName { get; set; } = null!;
    [JsonPropertyName("last_name")] public string LastName { get; set; } = null!;
    [JsonPropertyName("email")] public string Email { get; set; } = null!;
    [JsonPropertyName("password")] public string Password { get; set; } = null!;
};