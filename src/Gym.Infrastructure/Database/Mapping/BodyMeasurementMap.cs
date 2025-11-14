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
            .Property(b => b.MeasurementDate)
            .IsRequired(false)
            .HasDefaultValue(null)
            .HasColumnName("measurement_date")
            .HasColumnType("date");

        builder
            .Property(b => b.Weight)
            .IsRequired(false)
            .HasColumnName("weight")
            .HasColumnType("decimal");

        builder.Property(b => b.Height)
            .IsRequired(false)
            .HasColumnName("height")
            .HasColumnType("decimal");
        
        builder.Property(b => b.BodyFat)
            .IsRequired(false)
            .HasColumnName("body_fat")
            .HasColumnType("decimal");
        
        builder.Property(b => b.Chest)
            .IsRequired(false)
            .HasColumnName("chest")
            .HasColumnType("decimal");
        
        builder.Property(b => b.Waist)
            .IsRequired(false)
            .HasColumnName("waist")
            .HasColumnType("decimal");
        
        builder.Property(b => b.Hips)
            .IsRequired(false)
            .HasColumnName("hips")
            .HasColumnType("decimal");
        
        builder.Property(b => b.Biceps)
            .IsRequired(false)
            .HasColumnName("biceps")
            .HasColumnType("decimal");
        
        builder.Property(b => b.Thighs) 
            .IsRequired(false)
            .HasColumnName("thighs")
            .HasColumnType("decimal");
        
        builder.Property(b => b.Calves)
            .IsRequired(false)
            .HasColumnName("calves")
            .HasColumnType("decimal");
        
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
            .HasDefaultValue(DateTime.UtcNow)
            .HasColumnName("created_at")
            .HasColumnType("datetime2");

        builder
            .Property(b => b.UpdatedAt)
            .IsRequired(true)
            .HasDefaultValue(DateTime.UtcNow)
            .HasColumnName("updated_at")
            .HasColumnType("datetime2");

        builder
            .HasOne(b => b.User)
            .WithMany(u => u.BodyMeasurements)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}