namespace Gym.Domain.Entities;

public class User : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public bool IsActive { get; private set; }
    
    public List<BodyMeasurement> BodyMeasurements { get; private set; }
    public List<WorkoutPlan> WorkoutPlans { get; private set; }
    public List<WorkoutSession> WorkoutSessions { get; private set; }

    public User(string firstName, string lastName, string email, string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);
        
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = email.Trim();
        PasswordHash = passwordHash.Trim();
        IsActive = true;
    }

    public void UpdateFirstName(string newFirstName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newFirstName);

        this.FirstName = newFirstName.Trim();
        RefreshUpdatedAt();
    }

    public void UpdateLastName(string newLastName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newLastName);

        this.LastName = newLastName.Trim();
        RefreshUpdatedAt();
    }

    public void UpdateEmail(string newEmail)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newEmail);

        this.Email = newEmail.Trim();
        RefreshUpdatedAt();
    }
    
    public void UpdatePasswordHash(string newPasswordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newPasswordHash);

        this.PasswordHash = newPasswordHash.Trim();
        RefreshUpdatedAt();
    }

}