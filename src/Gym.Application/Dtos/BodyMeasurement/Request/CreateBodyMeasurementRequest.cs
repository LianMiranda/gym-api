namespace Gym.Application.Dtos.BodyMeasurement.Request;

public record CreateBodyMeasurementRequest
{
    public Guid UserId { get; set; }
    public string Name { get; set; } = null!;
    public DateOnly? MeasurementDate { get; set; }
    public decimal? Weight { get; set; }
    public decimal? Height { get; set; }
    public decimal? BodyFat { get; set; }
    public decimal? Chest { get; set; }
    public decimal? Waist { get; set; }
    public decimal? Hips { get; set; }
    public decimal? Biceps { get; set; }
    public decimal? Thighs { get; set; }
    public decimal? Calves { get; set; }
    public string? Notes { get; set; }
}