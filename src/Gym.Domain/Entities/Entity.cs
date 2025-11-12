namespace Gym.Domain.Entities;

public abstract class Entity
{
    public Guid Id { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    protected Entity()
    {
        Id = Guid.CreateVersion7();
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    protected void RefreshUpdatedAt()
    {
        this.UpdatedAt = DateTime.UtcNow;
    }
}