using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Database.Mapping;

public class WorkoutSessionMap : IEntityTypeConfiguration<WorkoutSession>
{
    public void Configure(EntityTypeBuilder<WorkoutSession> builder)
    {
        builder
            .ToTable("workout_sessions");

        builder
            .HasKey(w => w.Id);
        
        builder
            .Property(w => w.Id)
            .IsRequired(true)
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        builder
            .Property(w => w.UserId)
            .IsRequired(true)
            .HasColumnName("user_id")
            .HasColumnType("uniqueidentifier");
        
        builder
            .Property(w => w.RoutineId)
            .IsRequired(true)
            .HasColumnName("routine_id")
            .HasColumnType("uniqueidentifier");
        
        builder
            .Property(w => w.StartTime)
            .IsRequired(true)
            .HasColumnName("start_time")
            .HasColumnType("datetime2");
        
        builder
            .Property(w => w.DurationMinutes)
            .IsRequired(true)
            .HasColumnName("duration_minutes")
            .HasColumnType("time");
        
        builder
            .Property(w => w.Notes)
            .IsRequired(false)
            .HasColumnName("notes")
            .HasColumnType("text");
        
        builder
            .Property(w => w.CreatedAt)
            .IsRequired(true)
            .HasDefaultValue(DateTime.UtcNow)
            .HasColumnName("created_at")
            .HasColumnType("datetime2");

        builder
            .Property(w => w.UpdatedAt)
            .IsRequired(true)
            .HasDefaultValue(DateTime.UtcNow)
            .HasColumnName("updated_at")
            .HasColumnType("datetime2");

        builder
            .HasOne(w => w.User)
            .WithMany(w => w.WorkoutSessions)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder
            .HasOne(w => w.Routine)
            .WithMany(w => w.WorkoutSessions)
            .HasForeignKey(w => w.RoutineId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}