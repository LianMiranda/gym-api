using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Database.Mapping;

public class RoutineExerciseMap : IEntityTypeConfiguration<RoutineExercise>
{
    public void Configure(EntityTypeBuilder<RoutineExercise> builder)
    {
        builder
            .ToTable("routine_exercise");

        builder
            .HasKey(r => r.Id);

        builder
            .Property(r => r.Id)
            .IsRequired(true)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("id");
        
        builder
            .Property(r => r.RoutineId)
            .IsRequired(true)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("routine_id");
        
        builder
            .Property(r => r.ExerciseId)
            .IsRequired(true)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("exercise_id");
        
        builder
            .Property(r => r.OrderIndex)
            .IsRequired(true)
            .HasColumnType("tinyint")
            .HasColumnName("order_index");

        builder
            .Property(r => r.Notes)
            .IsRequired(false)
            .HasColumnType("text")
            .HasColumnName("notes");

        builder
            .Property(r => r.CreatedAt)
            .IsRequired(true)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnType("datetime2")
            .HasColumnName("created_at");
        
        builder
            .Property(r => r.UpdatedAt)
            .IsRequired(true)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnType("datetime2")
            .HasColumnName("updated_at");

        builder
            .HasOne(r => r.Exercise)
            .WithMany(e => e.RoutineExercises)
            .HasForeignKey(r => r.ExerciseId)
            .OnDelete(DeleteBehavior.Restrict);
        
        
        builder
            .HasOne(r => r.Routine)
            .WithMany(e => e.RoutineExercises)
            .HasForeignKey(r => r.RoutineId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}