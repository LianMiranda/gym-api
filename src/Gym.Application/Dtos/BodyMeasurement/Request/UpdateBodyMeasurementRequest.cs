using System.Text.Json.Serialization;

namespace Gym.Application.Dtos.BodyMeasurement.Request;

public record UpdateBodyMeasurementRequest
{
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("measurement_date")] public DateOnly? MeasurementDate { get; set; }
    [JsonPropertyName("weight")] public decimal? Weight { get; set; }
    [JsonPropertyName("height")] public decimal? Height { get; set; }
    [JsonPropertyName("body_fat")] public decimal? BodyFat { get; set; }
    [JsonPropertyName("chest")] public decimal? Chest { get; set; }
    [JsonPropertyName("waist")] public decimal? Waist { get; set; }
    [JsonPropertyName("hips")] public decimal? Hips { get; set; }
    [JsonPropertyName("biceps")] public decimal? Biceps { get; set; }
    [JsonPropertyName("thighs")] public decimal? Thighs { get; set; }
    [JsonPropertyName("calves")] public decimal? Calves { get; set; }
    [JsonPropertyName("notes")] public string? Notes { get; set; }
};