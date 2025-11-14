using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gym.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; private set; } = null!;
    public DbSet<BodyMeasurement> BodyMeasurements { get; private set; } = null!;
    public DbSet<Exercise> Exercises { get; private set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}