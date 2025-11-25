using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Gym.Application.Dtos.Auth.Request;

public record LoginRequest
{
    [Required]
    [EmailAddress]
    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;

    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;
};