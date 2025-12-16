using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Database.Mapping;

public class RoutineMap : IEntityTypeConfiguration<Routine>
{
    public void Configure(EntityTypeBuilder<Routine> builder)
    {
        builder
            .ToTable("routines");

        builder
            .HasKey(r => r.Id);
        
        builder
            .Property(r => r.Id)
            .IsRequired(true)
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        builder
            .Property(r => r.WorkoutPlanId)
            .IsRequired(true)
            .HasColumnName("workout_plan_id")
            .HasColumnType("uniqueidentifier");
        
        builder
            .Property(r => r.Name)
            .IsRequired(true)
            .HasColumnName("name")
            .HasColumnType("nvarchar")
            .HasMaxLength(100);
        
        builder
            .Property(r => r.Description)
            .IsRequired(false)
            .HasColumnName("description")
            .HasColumnType("text");
        
        builder
            .Property(r => r.ImageUrl)
            .IsRequired(false)
            .HasColumnName("image_url")
            .HasColumnType("text");
        
        builder
            .Property(r => r.OrderIndex)
            .IsRequired(true)
            .HasColumnName("order_index")
            .HasColumnType("tinyint");
        
        builder
            .Property(r => r.CreatedAt)
            .IsRequired(true)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnName("created_at")
            .HasColumnType("datetime2");

        builder
            .Property(r => r.UpdatedAt)
            .IsRequired(true)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnName("updated_at")
            .HasColumnType("datetime2");

        builder
            .HasOne(r => r.WorkoutPlan)
            .WithMany(w => w.Routines)
            .HasForeignKey(r => r.WorkoutPlanId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}