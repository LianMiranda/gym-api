using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gym.Application.Dtos.User.Request;

public record UpdateUserRequest()
{
    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("email")]
    [EmailAddress]
    public string? Email { get; set; }

    [JsonPropertyName("password")]
    public string? Password { get; set; }
}