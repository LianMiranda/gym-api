using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Database.Mapping;

public class ExerciseSetMap : IEntityTypeConfiguration<ExerciseSet>
{
    public void Configure(EntityTypeBuilder<ExerciseSet> builder)
    {
        builder
            .ToTable("exercise_sets");

        builder
            .HasKey(e => e.Id);
        
        builder
            .Property(e => e.Id)
            .IsRequired(true)
            .HasColumnName("id")
            .HasColumnType("uniqueidentifier");

        builder
            .Property(e => e.RoutineExerciseId)
            .IsRequired(true)
            .HasColumnName("routine_exercise_id")
            .HasColumnType("uniqueidentifier");
        
        builder
            .Property(e => e.SetNumber)
            .IsRequired(true)
            .HasColumnName("set_number")
            .HasColumnType("tinyint");
        
        builder
            .Property(e => e.Reps)
            .IsRequired(true)
            .HasDefaultValue(0)
            .HasColumnName("reps")
            .HasColumnType("smallint");
        
        builder
            .Property(e => e.Weight)
            .IsRequired(true)
            .HasDefaultValue(0)
            .HasColumnName("weight")
            .HasColumnType("decimal(5,2)");
        
        builder
            .Property(e => e.RestTimeSeconds)
            .IsRequired(false)
            .HasColumnName("rest_time_seconds")
            .HasColumnType("int");
        
        builder
            .Property(e => e.Completed)
            .IsRequired(true)
            .HasDefaultValue(false)
            .HasColumnName("completed")
            .HasColumnType("bit");
        
        builder
            .Property(e => e.Type)
            .IsRequired(true)
            .HasColumnName("type")
            .HasConversion<string>()
            .HasColumnType("varchar")
            .HasMaxLength(50);
        
        builder
            .Property(e => e.Rpe)
            .IsRequired(false)
            .HasColumnName("rpe")
            .HasColumnType("decimal(3,1)");
        
        builder
            .Property(e => e.CreatedAt)
            .IsRequired(true)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnName("created_at")
            .HasColumnType("datetime2");

        builder
            .Property(e => e.UpdatedAt)
            .IsRequired(true)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnName("updated_at")
            .HasColumnType("datetime2");
        
        builder.HasIndex(e => e.RoutineExerciseId);
        builder.HasIndex(e => new { e.RoutineExerciseId, e.SetNumber })
            .IsUnique();

        builder
            .HasOne(e => e.RoutineExercise)
            .WithMany(r => r.Sets)
            .HasForeignKey(e => e.RoutineExerciseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}