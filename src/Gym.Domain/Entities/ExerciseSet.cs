using Gym.Domain.Enums.ExerciseSet_Enums;

namespace Gym.Domain.Entities;

public class ExerciseSet : Entity
{
    public Guid RoutineExerciseId { get; private set; }
    public int SetNumber { get; private set; }
    public int Reps { get; private set; }
    public decimal Weight { get; private set; }
    public short? RestTimeSeconds { get; private set; }
    public bool Completed { get; private set; }
    public SetType Type { get; private set; }
    public double? Rpe { get; private set; }

    public RoutineExercise RoutineExercise { get; set; }

    public ExerciseSet(Guid routineExerciseId, int setNumber, int reps, decimal weight, short? restTimeSeconds,
        bool completed, SetType type, double? rpe)
    {
        if (routineExerciseId == Guid.Empty)
            throw new ArgumentException("RoutineExerciseId cannot be empty.", nameof(routineExerciseId));
        if (setNumber <= 0)
            throw new ArgumentException("SetNumber must be greater than zero.", nameof(setNumber));
        if (reps < 0)
            throw new ArgumentException("Reps cannot be negative.", nameof(reps));
        if (weight < 0)
            throw new ArgumentException("Weight cannot be negative.", nameof(weight));
        if (restTimeSeconds < 0)
            throw new ArgumentException("RestTimeSeconds cannot be negative.", nameof(restTimeSeconds));
        if (!Enum.IsDefined(typeof(SetType), type))
            throw new ArgumentException("Invalid set type.", nameof(type));
        if (rpe.HasValue && (rpe.Value < 0 || rpe.Value > 10))
            throw new ArgumentException("RPE must be between 0 and 10.", nameof(rpe));

        RoutineExerciseId = routineExerciseId;
        SetNumber = setNumber;
        Reps = reps;
        Weight = weight;
        RestTimeSeconds = restTimeSeconds;
        Completed = completed;
        Type = type;
        Rpe = rpe;
    }

    public void UpdateSetNumber(int newSetNumber)
    {
        if (newSetNumber <= 0)
            throw new ArgumentException("SetNumber must be greater than zero.", nameof(newSetNumber));

        this.SetNumber = newSetNumber;
        RefreshUpdatedAt();
    }

    public void UpdateReps(int newReps)
    {
        if (newReps < 0)
            throw new ArgumentException("Reps cannot be negative.", nameof(newReps));

        this.Reps = newReps;
        RefreshUpdatedAt();
    }

    public void UpdateWeight(decimal newWeight)
    {
        if (newWeight < 0)
            throw new ArgumentException("Weight cannot be negative.", nameof(newWeight));

        this.Weight = newWeight;
        RefreshUpdatedAt();
    }

    public void UpdateRestTimeSeconds(short newRestTimeSeconds)
    {
        if (newRestTimeSeconds < 0)
            throw new ArgumentException("RestTimeSeconds cannot be negative.", nameof(newRestTimeSeconds));

        this.RestTimeSeconds = newRestTimeSeconds;
        RefreshUpdatedAt();
    }

    public void UpdateCompleted(bool newCompleted)
    {
        this.Completed = newCompleted;
        RefreshUpdatedAt();
    }

    public void UpdateType(SetType newType)
    {
        if (!Enum.IsDefined(typeof(SetType), newType))
            throw new ArgumentException("Invalid set type.", nameof(newType));

        this.Type = newType;
        RefreshUpdatedAt();
    }

    public void UpdateRpe(double? newRpe)
    { 
        if (newRpe.HasValue && (newRpe.Value < 0 || newRpe.Value > 10))
            throw new ArgumentException("RPE must be between 0 and 10.", nameof(newRpe));

        this.Rpe = newRpe;
        RefreshUpdatedAt();
    }
}