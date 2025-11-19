using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gym.Application.Dtos.User;

public record CreateUserRequest
{
    [JsonPropertyName("first_name")]
    [Required]
    public string FirstName { get; set; } = null!;

    [JsonPropertyName("last_name")]
    [Required]
    public string LastName { get; set; } = null!;

    [JsonPropertyName("email")]
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;

    [JsonPropertyName("password")]
    [Required]
    public string Password { get; set; } = null!;
};