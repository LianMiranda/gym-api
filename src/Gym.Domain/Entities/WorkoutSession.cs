namespace Gym.Domain.Entities;

public class WorkoutSession : Entity
{
    public Guid UserId { get; private set; }
    public Guid RoutineId { get; private set; }
    public DateTime StartTime { get; private set; }
    public TimeSpan DurationMinutes { get; private set; }
    public string? Notes { get; private set; }

    public User User { get; private set; }
    public Routine Routine { get; private set; }

    public WorkoutSession(Guid userId, Guid routineId, DateTime startTime, TimeSpan durationMinutes, string? notes)
    {
        if(userId == Guid.Empty)  throw new ArgumentException("UserId cannot be empty", nameof(userId)); 
        if(routineId == Guid.Empty)  throw new ArgumentException("RoutineId cannot be empty", nameof(routineId)); 
        if (startTime == default)
            throw new ArgumentException("StartTime cannot be default value", nameof(startTime));
        if (durationMinutes <= TimeSpan.Zero)
            throw new ArgumentException("Duration must be greater than zero", nameof(durationMinutes));
        
        UserId = userId;
        RoutineId = routineId;
        StartTime = startTime;
        DurationMinutes = durationMinutes;
        Notes = notes?.Trim();
    }

    public void UpdateStartTime(DateTime newStartTime)
    {
        if (newStartTime == default)
            throw new ArgumentException("StartTime cannot be default value", nameof(newStartTime));
        
        this.StartTime = newStartTime;
        RefreshUpdatedAt();
    }
    
    public void UpdateDurationMinutes(TimeSpan newDurationMinutes)
    {
        if (newDurationMinutes <= TimeSpan.Zero)
            throw new ArgumentException("Duration must be greater than zero", nameof(newDurationMinutes));
        
        this.DurationMinutes = newDurationMinutes;
        RefreshUpdatedAt();
    }
    
    public void UpdateNotes(string newNotes)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newNotes);
        
        this.Notes = newNotes.Trim();
        RefreshUpdatedAt();
    }
    
}