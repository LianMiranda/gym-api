using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Database.Mapping;

public class WorkoutPlanMap : IEntityTypeConfiguration<WorkoutPlan>
{
    public void Configure(EntityTypeBuilder<WorkoutPlan> builder)
    {
        builder
            .ToTable("workout_plans");

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
            .Property(w => w.Name)
            .IsRequired(true)
            .HasColumnName("name")
            .HasColumnType("nvarchar")
            .HasMaxLength(100);
        
        builder
            .Property(w => w.Description)
            .IsRequired(false)
            .HasColumnName("description")
            .HasColumnType("text");
        
        builder
            .Property(w => w.DaysPerWeek)
            .IsRequired(false)
            .HasColumnName("days_per_week")
            .HasColumnType("tinyint");
        
        builder
            .Property(w => w.Months)
            .IsRequired(false)
            .HasColumnName("months")
            .HasColumnType("tinyint");
        
        builder
            .Property(w => w.Goal)
            .IsRequired(false)
            .HasColumnName("goal")
            .HasConversion<string>()
            .HasColumnType("varchar")
            .HasMaxLength(15);
        
        builder
            .Property(u => u.CreatedAt)
            .IsRequired(true)
            .HasDefaultValue("GETUTCDATE()")
            .HasColumnName("created_at")
            .HasColumnType("datetime2");

        builder
            .Property(u => u.UpdatedAt)
            .IsRequired(true)
            .HasDefaultValue("GETUTCDATE()")
            .HasColumnName("updated_at")
            .HasColumnType("datetime2");

        builder
            .HasOne(w => w.User)
            .WithMany(u => u.WorkoutPlans)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}