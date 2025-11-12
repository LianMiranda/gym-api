namespace Gym.Domain.Entities;

public class BodyMeasurement : Entity
{
    public Guid UserId { get; private set; }
    public DateOnly? MeasurementDate { get; private set; }
    public decimal? Weight { get; private set; }
    public decimal? Height { get; private set; }
    public decimal? BodyFat { get; private set; }
    public decimal? Chest { get; private set; }
    public decimal? Waist { get; private set; }
    public decimal? Hips { get; private set; }
    public decimal? Biceps { get; private set; }
    public decimal? Thighs { get; private set; }
    public decimal? Calves { get; private set; }
    public string? Notes { get; private set; }
    public User User { get; private set; }

    public BodyMeasurement(Guid userId, DateOnly? measurementDate, decimal? weight, decimal? height, decimal? bodyFat,
        decimal? chest, decimal? waist, decimal? hips, decimal? biceps, decimal? thighs, decimal? calves, string? notes)
    {
        UserId = userId;
        MeasurementDate = measurementDate;
        Weight = weight;
        Height = height;
        BodyFat = bodyFat;
        Chest = chest;
        Waist = waist;
        Hips = hips;
        Biceps = biceps;
        Thighs = thighs;
        Calves = calves;
        Notes = notes;
    }

    public void UpdateMeasurementDate(DateOnly newMeasurementDate)
    {
        this.MeasurementDate = newMeasurementDate;
        RefreshUpdatedAt();
    }

    public void UpdateWeight(decimal newWeight)
    {
        this.Weight = newWeight;
        RefreshUpdatedAt();
    }

    public void UpdateHeight(decimal newHeight)
    {
        this.Height = newHeight;
        RefreshUpdatedAt();
    }

    public void UpdateBodyFat(decimal newBodyFat)
    {
        this.BodyFat = newBodyFat;
        RefreshUpdatedAt();
    }

    public void UpdateChest(decimal newChest)
    {
        this.Chest = newChest;
        RefreshUpdatedAt();
    }

    public void UpdateWaist(decimal newWaist)
    {
        this.Waist = newWaist;
        RefreshUpdatedAt();
    }

    public void UpdateHips(decimal newHips)
    {
        this.Hips = newHips;
        RefreshUpdatedAt();
    }

    public void UpdateBiceps(decimal newBiceps)
    {
        this.Biceps = newBiceps;
        RefreshUpdatedAt();
    }

    public void UpdateThighs(decimal newThighs)
    {
        this.Thighs = newThighs;
        RefreshUpdatedAt();
    }

    public void UpdateCalves(decimal newCalves)
    {
        this.Calves = newCalves;
        RefreshUpdatedAt();
    }

    public void UpdateNotes(string newNotes)
    {
        this.Notes = newNotes;
        RefreshUpdatedAt();
    }
}