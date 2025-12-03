namespace Gym.Domain.Entities;

public class BodyMeasurement : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
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

    public BodyMeasurement(Guid userId, string name, DateOnly? measurementDate, decimal? weight, decimal? height,
        decimal? bodyFat,
        decimal? chest, decimal? waist, decimal? hips, decimal? biceps, decimal? thighs, decimal? calves, string? notes)
    {
        if (userId == Guid.Empty) throw new ArgumentException("UserId cannot be empty", nameof(userId));
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (measurementDate > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Measurement date cannot be in the future.", nameof(measurementDate));
        ValidateDecimalValue(weight, nameof(weight));
        ValidateDecimalValue(height, nameof(height));
        ValidateDecimalValue(bodyFat, nameof(bodyFat));
        ValidateDecimalValue(chest, nameof(chest));
        ValidateDecimalValue(waist, nameof(waist));
        ValidateDecimalValue(hips, nameof(hips));
        ValidateDecimalValue(biceps, nameof(biceps));
        ValidateDecimalValue(thighs, nameof(thighs));
        ValidateDecimalValue(calves, nameof(calves));

        UserId = userId;
        Name = name;
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
        Notes = notes?.Trim();
    }

    public void UpdateName(string newName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newName);

        this.Name = newName;
        RefreshUpdatedAt();
    }

    public void UpdateMeasurementDate(DateOnly newMeasurementDate)
    {
        if (newMeasurementDate > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException("Measurement date cannot be in the future.", nameof(newMeasurementDate));

        this.MeasurementDate = newMeasurementDate;
        RefreshUpdatedAt();
    }

    public void UpdateWeight(decimal newWeight)
    {
        ValidateDecimalValue(newWeight, nameof(newWeight));

        this.Weight = newWeight;
        RefreshUpdatedAt();
    }

    public void UpdateHeight(decimal newHeight)
    {
        ValidateDecimalValue(newHeight, nameof(newHeight));

        this.Height = newHeight;
        RefreshUpdatedAt();
    }

    public void UpdateBodyFat(decimal newBodyFat)
    {
        ValidateDecimalValue(newBodyFat, nameof(newBodyFat));

        this.BodyFat = newBodyFat;
        RefreshUpdatedAt();
    }

    public void UpdateChest(decimal newChest)
    {
        ValidateDecimalValue(newChest, nameof(newChest));

        this.Chest = newChest;
        RefreshUpdatedAt();
    }

    public void UpdateWaist(decimal newWaist)
    {
        ValidateDecimalValue(newWaist, nameof(newWaist));

        this.Waist = newWaist;
        RefreshUpdatedAt();
    }

    public void UpdateHips(decimal newHips)
    {
        ValidateDecimalValue(newHips, nameof(newHips));

        this.Hips = newHips;
        RefreshUpdatedAt();
    }

    public void UpdateBiceps(decimal newBiceps)
    {
        ValidateDecimalValue(newBiceps, nameof(newBiceps));

        this.Biceps = newBiceps;
        RefreshUpdatedAt();
    }

    public void UpdateThighs(decimal newThighs)
    {
        ValidateDecimalValue(newThighs, nameof(newThighs));

        this.Thighs = newThighs;
        RefreshUpdatedAt();
    }

    public void UpdateCalves(decimal newCalves)
    {
        ValidateDecimalValue(newCalves, nameof(newCalves));

        this.Calves = newCalves;
        RefreshUpdatedAt();
    }

    public void UpdateNotes(string newNotes)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newNotes);

        this.Notes = newNotes.Trim();
        RefreshUpdatedAt();
    }

    private void ValidateDecimalValue(decimal? value, string fieldName)
    {
        if (value < 0)
            throw new ArgumentException($"{fieldName} cannot be negative");
    }
}