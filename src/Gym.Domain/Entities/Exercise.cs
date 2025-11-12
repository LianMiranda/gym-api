using Gym.Domain.Enums.Exercise_Enums;

namespace Gym.Domain.Entities;

public class Exercise : Entity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public ExerciseCategory Category { get; private set; }
    public MuscleGroup MuscleGroup { get; private set; }
    public Equipment Equipment { get; private set; }
    public DifficultyLevel DifficultyLevel { get; private set; }
    public string? ImageUrl { get; private set; }
    public string? VideoUrl { get; private set; }
    public string? ExternalApiId { get; private set; }

    public Exercise(string name, string description, ExerciseCategory category, MuscleGroup muscleGroup,
        Equipment equipment, DifficultyLevel difficultyLevel, string? imageUrl, string? videoUrl, string? externalApiId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        if (!Enum.IsDefined(typeof(ExerciseCategory), category))
            throw new ArgumentException("Invalid exercise category.", nameof(category));
        if (!Enum.IsDefined(typeof(MuscleGroup), muscleGroup))
            throw new ArgumentException("Invalid exercise category.", nameof(muscleGroup));
        if (!Enum.IsDefined(typeof(Equipment), equipment))
            throw new ArgumentException("Invalid exercise category.", nameof(equipment));
        if (!Enum.IsDefined(typeof(DifficultyLevel), difficultyLevel))
            throw new ArgumentException("Invalid exercise category.", nameof(difficultyLevel));

        Name = name.Trim();
        Description = description.Trim();
        Category = category;
        MuscleGroup = muscleGroup;
        Equipment = equipment;
        DifficultyLevel = difficultyLevel;
        ImageUrl = imageUrl?.Trim();
        VideoUrl = videoUrl?.Trim();
        ExternalApiId = externalApiId?.Trim();
    }

    public void UpdateName(string newName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newName);
        this.Name = newName.Trim();
        RefreshUpdatedAt();
    }

    public void UpdateDescription(string newDescription)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newDescription);
        this.Description = newDescription.Trim();
        RefreshUpdatedAt();
    }

    public void UpdateCategory(ExerciseCategory newCategory)
    {
        if (!Enum.IsDefined(typeof(ExerciseCategory), newCategory))
            throw new ArgumentException("Invalid exercise category.", nameof(newCategory));

        this.Category = newCategory;
        RefreshUpdatedAt();
    }

    public void UpdateMuscleGroup(MuscleGroup newMuscleGroup)
    {
        if (!Enum.IsDefined(typeof(ExerciseCategory), newMuscleGroup))
            throw new ArgumentException("Invalid muscle group.", nameof(newMuscleGroup));

        this.MuscleGroup = newMuscleGroup;
        RefreshUpdatedAt();
    }

    public void UpdateEquipment(Equipment newEquipment)
    {
        if (!Enum.IsDefined(typeof(ExerciseCategory), newEquipment))
            throw new ArgumentException("Invalid equipment.", nameof(newEquipment));

        this.Equipment = newEquipment;
        RefreshUpdatedAt();
    }

    public void UpdateDifficultyLevel(DifficultyLevel newDifficultyLevel)
    {
        if (!Enum.IsDefined(typeof(ExerciseCategory), newDifficultyLevel))
            throw new ArgumentException("Invalid difficulty level.", nameof(newDifficultyLevel));

        this.DifficultyLevel = newDifficultyLevel;
        RefreshUpdatedAt();
    }

    public void UpdateImageUrl(string newImageUrl)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newImageUrl);

        this.ImageUrl = newImageUrl.Trim();
        RefreshUpdatedAt();
    }

    public void UpdateVideoUrl(string newVideoUrl)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newVideoUrl);

        this.VideoUrl = newVideoUrl.Trim();
        RefreshUpdatedAt();
    }
}