using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Database.Mapping;

public class ExerciseMap : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder
            .ToTable("exercise");

        builder
            .HasKey(e => e.Id);

        builder
            .Property(e => e.Id)
            .IsRequired(true)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("id");

        builder
            .Property(e => e.Name)
            .IsRequired(true)
            .HasColumnName("name")
            .HasColumnType("nvarchar");

        builder
            .Property(e => e.Description)
            .IsRequired(true)
            .HasColumnName("description")
            .HasColumnType("text");

        builder
            .Property(e => e.Category)
            .IsRequired(true)
            .HasColumnName("cateogry")
            .HasColumnType("smallint");

        builder
            .Property(e => e.MuscleGroup)
            .IsRequired(true)
            .HasColumnName("muscle_group")
            .HasColumnType("smallint");
        
        builder
            .Property(e => e.Equipment)
            .IsRequired(true)
            .HasColumnName("equipment")
            .HasColumnType("smallint");

        builder
            .Property(e => e.DifficultyLevel)
            .IsRequired(true)
            .HasColumnName("difficulty_level")
            .HasColumnType("smallint");

        builder
            .Property(e => e.ImageUrl)
            .IsRequired(false)
            .HasColumnName("image_url")
            .HasColumnType("nvarchar");

        builder
            .Property(e => e.VideoUrl)
            .IsRequired(false)
            .HasColumnName("video_url")
            .HasColumnType("nvarchar");

        builder
            .Property(e => e.ExternalApiId)
            .IsRequired(false)
            .HasColumnName("external_api_id")
            .HasColumnType("nvarchar");

        builder
            .Property(u => u.CreatedAt)
            .IsRequired(true)
            .HasDefaultValue(DateTime.UtcNow)
            .HasColumnName("created_at")
            .HasColumnType("datetime2");

        builder
            .Property(u => u.UpdatedAt)
            .IsRequired(true)
            .HasDefaultValue(DateTime.UtcNow)
            .HasColumnName("updated_at")
            .HasColumnType("datetime2");
    }
}