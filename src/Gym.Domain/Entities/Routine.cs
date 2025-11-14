namespace Gym.Domain.Entities;

public class Routine : Entity
{
    public Guid WorkoutPlanId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? ImageUrl { get; private set; }
    public int OrderIndex { get; private set; }
    
    public WorkoutPlan WorkoutPlan { get; private set; }
    public List<RoutineExercise> RoutineExercises { get; private set; }
    public List<WorkoutSession> WorkoutSessions { get; private set; }

    public Routine(Guid workoutPlanId, string name, string? description, string? imageUrl, int orderIndex)
    {
        if(workoutPlanId == Guid.Empty)  throw new ArgumentException("WorkoutPlanId cannot be empty", nameof(workoutPlanId)); 
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (orderIndex < 0)
            throw new ArgumentException("The order index must be equal to or greater than 0.", nameof(orderIndex));


        WorkoutPlanId = workoutPlanId;
        Name = name.Trim();
        Description = description?.Trim();
        ImageUrl = imageUrl?.Trim();
        OrderIndex = orderIndex;
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

    public void UpdateImageUrl(string newImageUrl)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newImageUrl);
        this.ImageUrl = newImageUrl.Trim();
        RefreshUpdatedAt();
    }

    public void UpdateOrderIndex(int newOrderIndex)
    {
        if (newOrderIndex < 0)
            throw new ArgumentException("The order index must be equal to or greater than 0.", nameof(newOrderIndex));

        this.OrderIndex = newOrderIndex;
        RefreshUpdatedAt();
    }
}