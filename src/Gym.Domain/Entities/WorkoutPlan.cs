using Gym.Domain.Enums;

namespace Gym.Domain.Entities;

public class WorkoutPlan : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public int DaysPerWeek { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public Goal Goal { get; set; }

    public User User { get; private set; }
    public List<Routine> Routines { get; set; }

    public WorkoutPlan(Guid userId, string name, string? description, int daysPerWeek, DateOnly startDate,
        DateOnly endDate, Goal goal)
    {
        if (userId == Guid.Empty) throw new ArgumentException("UserId cannot be empty", nameof(userId));
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        if (daysPerWeek <= 0 || daysPerWeek > 7)
            throw new ArgumentOutOfRangeException(nameof(daysPerWeek),
                "The number of days per week should be between 1 and 7.");
        if (startDate < DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentException("The start date cannot be earlier than today.", nameof(startDate));
        if (endDate <= StartDate)
            throw new ArgumentException("The end date must be later than the start date.", nameof(endDate));
        if (!Enum.IsDefined(typeof(Goal), goal))
            throw new ArgumentException("Invalid goal.", nameof(goal));

        UserId = userId;
        Name = name.Trim();
        Description = description.Trim();
        DaysPerWeek = daysPerWeek;
        StartDate = startDate;
        EndDate = endDate;
        Goal = goal;
    }

    public void UpdateName(string newName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newName);
        this.Name = newName;

        RefreshUpdatedAt();
    }

    public void UpdateDescription(string newDescription)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newDescription);
        this.Description = newDescription;

        RefreshUpdatedAt();
    }

    public void UpdateDaysPerWeek(int newDaysPerWeek)
    {
        if (newDaysPerWeek <= 0 || newDaysPerWeek > 7)
            throw new ArgumentOutOfRangeException(nameof(newDaysPerWeek),
                "The number of days per week should be between 1 and 7.");

        this.DaysPerWeek = newDaysPerWeek;

        RefreshUpdatedAt();
    }

    public void UpdateStartDate(DateOnly newStartDate)
    {
        if (newStartDate < DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentException("The start date cannot be earlier than today.", nameof(newStartDate));

        this.StartDate = newStartDate;

        RefreshUpdatedAt();
    }

    public void UpdateEndDate(DateOnly newEndDate)
    {
        if (newEndDate <= this.StartDate)
            throw new ArgumentException("The end date must be later than the start date.", nameof(newEndDate));

        this.EndDate = newEndDate;

        RefreshUpdatedAt();
    }
    
    public void UpdateGoal(Goal newGoal)
    {
        if (!Enum.IsDefined(typeof(Goal), newGoal))
            throw new ArgumentException("Invalid goal.", nameof(newGoal));

        this.Goal = newGoal;

        RefreshUpdatedAt();
    }
}