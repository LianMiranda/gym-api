using Gym.Domain.Enums;

namespace Gym.Domain.Entities;

public class WorkoutPlan : Entity
{
    public Guid UserId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public sbyte? DaysPerWeek { get; private set; }
    public sbyte? Months { get; private set; }
    public Goal? Goal { get; set; }

    public User User { get; private set; }
    public List<Routine> Routines { get; private set; }

    public WorkoutPlan(Guid userId, string name, string? description, sbyte? daysPerWeek, sbyte? months, Goal? goal)
    {
        if (userId == Guid.Empty) throw new ArgumentException("UserId cannot be empty", nameof(userId));
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        if (daysPerWeek.HasValue)
        {
            if (daysPerWeek <= 0 || daysPerWeek > 7)
                throw new ArgumentOutOfRangeException(nameof(daysPerWeek),
                    "The number of days per week should be between 1 and 7.");
        }
        if (months.HasValue)
        {
            if (months < 0)
                throw new ArgumentException("The number of months cannot be negative.", nameof(months));
        }
        if (goal.HasValue)
        {
            if (!Enum.IsDefined(typeof(Goal), goal))
                throw new ArgumentException("Invalid goal.", nameof(goal));
        }

        UserId = userId;
        Name = name.Trim();
        Description = description.Trim();
        DaysPerWeek = daysPerWeek;
        Months = months;
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

    public void UpdateDaysPerWeek(sbyte newDaysPerWeek)
    {
        if (newDaysPerWeek <= 0 || newDaysPerWeek > 7)
            throw new ArgumentOutOfRangeException(nameof(newDaysPerWeek),
                "The number of days per week should be between 1 and 7.");

        this.DaysPerWeek = newDaysPerWeek;

        RefreshUpdatedAt();
    }

    public void UpdateMonths(sbyte newMonths)
    {
        if (newMonths < 0)
            throw new ArgumentException("The number of months cannot be negative.", nameof(newMonths));

        this.Months = newMonths;

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