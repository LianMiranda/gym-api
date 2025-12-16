namespace Gym.Domain.Entities;

public class RoutineExercise : Entity
{
    public Guid RoutineId { get; private set; }
    public Guid ExerciseId { get; private set; }
    public int OrderIndex { get; private set; }
    public string? Notes { get; private set; }

    public Routine Routine { get; private set; }
    public Exercise Exercise { get; private set; }
    public List<ExerciseSet> Sets { get; set; }


    public RoutineExercise(Guid routineId, Guid exerciseId, int orderIndex, string? notes)
    {
        if (routineId == Guid.Empty) throw new ArgumentException("RoutineId cannot be empty", nameof(routineId));
        if (exerciseId == Guid.Empty) throw new ArgumentException("ExerciseId cannot be empty", nameof(exerciseId));
        if (orderIndex < 0)
            throw new ArgumentException("The order index must be equal to or greater than 0.", nameof(orderIndex));

        RoutineId = routineId;
        ExerciseId = exerciseId;
        OrderIndex = orderIndex;
        Notes = notes?.Trim();
    }

    public void UpdateOrderIndex(int newOrderIndex)
    {
        if (newOrderIndex < 0)
            throw new ArgumentException("The order index must be equal to or greater than 0.", nameof(newOrderIndex));

        this.OrderIndex = newOrderIndex;
        RefreshUpdatedAt();
    }

    public void UpdateNotes(string newNotes)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newNotes);

        this.Notes = newNotes.Trim();
        RefreshUpdatedAt();
    }
}