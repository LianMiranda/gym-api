using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Database.Mapping;

public class ExerciseMap : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder
            .ToTable("exercises");

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
            .HasColumnType("nvarchar")
            .HasMaxLength(200);

        builder
            .Property(e => e.Description)
            .IsRequired(true)
            .HasColumnName("description")
            .HasColumnType("text");

        builder
            .Property(e => e.Category)
            .IsRequired(true)
            .HasColumnName("category")
            .HasConversion<string>()
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder
            .Property(e => e.MuscleGroup)
            .IsRequired(true)
            .HasColumnName("muscle_group")
            .HasConversion<string>()
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder
            .Property(e => e.Equipment)
            .IsRequired(true)
            .HasColumnName("equipment")
            .HasConversion<string>()
            .HasColumnType("varchar")
            .HasMaxLength(100);

        builder
            .Property(e => e.DifficultyLevel)
            .IsRequired(true)
            .HasColumnName("difficulty_level")
            .HasConversion<string>()
            .HasColumnType("varchar")
            .HasMaxLength(50);

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
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnName("created_at")
            .HasColumnType("datetime2");

        builder
            .Property(u => u.UpdatedAt)
            .IsRequired(true)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnName("updated_at")
            .HasColumnType("datetime2");
    }
}