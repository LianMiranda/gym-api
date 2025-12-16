using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Database.Mapping;

public class BodyMeasurementMap : IEntityTypeConfiguration<BodyMeasurement>
{
    public void Configure(EntityTypeBuilder<BodyMeasurement> builder)
    {
        builder
            .ToTable("body_measurements");

        builder
            .HasKey(u => u.Id);

        builder
            .Property(b => b.Id)
            .IsRequired(true)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("id");

        builder
            .Property(b => b.Name)
            .IsRequired(true)
            .HasColumnName("name")
            .HasColumnType("varchar")
            .HasMaxLength(100);
        
        builder
            .Property(b => b.MeasurementDate)
            .IsRequired(false)
            .HasColumnName("measurement_date")
            .HasColumnType("date");

        builder.Property(b => b.Weight)
            .HasColumnName("weight")
            .HasColumnType("decimal(5,2)");

        builder.Property(b => b.Height)
            .HasColumnName("height")
            .HasColumnType("decimal(5,2)");

        builder.Property(b => b.BodyFat)
            .HasColumnName("body_fat")
            .HasColumnType("decimal(5,2)");

        builder.Property(b => b.Chest)
            .HasColumnName("chest")
            .HasColumnType("decimal(5,2)");

        builder.Property(b => b.Waist)
            .HasColumnName("waist")
            .HasColumnType("decimal(5,2)");

        builder.Property(b => b.Hips)
            .HasColumnName("hips")
            .HasColumnType("decimal(5,2)");

        builder.Property(b => b.Biceps)
            .HasColumnName("biceps")
            .HasColumnType("decimal(5,2)");

        builder.Property(b => b.Thighs)
            .HasColumnName("thighs")
            .HasColumnType("decimal(5,2)");

        builder.Property(b => b.Calves)
            .HasColumnName("calves")
            .HasColumnType("decimal(5,2)");

        builder
            .Property(b => b.Notes)
            .IsRequired(false)
            .HasColumnName("notes")
            .HasColumnType("text");

        builder
            .Property(b => b.UserId)
            .IsRequired(true)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("user_id");

        builder
            .Property(b => b.CreatedAt)
            .IsRequired(true)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnName("created_at")
            .HasColumnType("datetime2");

        builder
            .Property(b => b.UpdatedAt)
            .IsRequired(true)
            .HasDefaultValueSql("GETUTCDATE()")
            .HasColumnName("updated_at")
            .HasColumnType("datetime2");

        builder
            .HasOne(b => b.User)
            .WithMany(u => u.BodyMeasurements)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}