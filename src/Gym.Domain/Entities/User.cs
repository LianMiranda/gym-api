namespace Gym.Domain.Entities;

public class User : Entity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public bool IsActive { get; private set; }
    
    public List<BodyMeasurement> BodyMeasurements { get; private set; }

    public User(string firstName, string lastName, string email, string passwordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(firstName);
        ArgumentException.ThrowIfNullOrWhiteSpace(lastName);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);
        ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);
        
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
        IsActive = true;
    }

    public void UpdateFirstName(string newFirstName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newFirstName);

        this.FirstName = newFirstName;
        RefreshUpdatedAt();
    }

    public void UpdateLastName(string newLastName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newLastName);

        this.LastName = newLastName;
        RefreshUpdatedAt();
    }

    public void UpdateEmail(string newEmail)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newEmail);

        this.Email = newEmail;
        RefreshUpdatedAt();
    }
    
    public void UpdatePasswordHash(string newPasswordHash)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newPasswordHash);

        this.PasswordHash = newPasswordHash;
        RefreshUpdatedAt();
    }

}