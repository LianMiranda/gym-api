using Gym.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Infrastructure.Database.Mapping;

public class UserTableMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .ToTable("users");
        
        builder
            .HasKey(u => u.Id);

        builder
            .Property(u => u.Id)
            .IsRequired(true)
            .HasColumnType("uniqueidentifier")
            .HasColumnName("id");

        builder
            .Property(u => u.FirstName)
            .IsRequired(true)
            .HasColumnName("first_name")
            .HasColumnType("varchar")
            .HasMaxLength(50);

        builder
            .Property(u => u.LastName)
            .IsRequired(true)
            .HasColumnName("last_name")
            .HasColumnType("varchar")
            .HasMaxLength(60);

        builder
            .Property(u => u.Email)
            .IsRequired(true)
            .HasColumnName("email")
            .HasColumnType("nvarchar")
            .IsUnicode()
            .HasMaxLength(255);
        
        builder
            .HasIndex(u => u.Email)
            .IsUnique();
        
        builder
            .Property(u => u.PasswordHash)
            .IsRequired(true)
            .HasColumnName("password_hash")
            .HasColumnType("nvarchar")
            .HasMaxLength(255);

        builder
            .Property(u => u.IsActive)
            .IsRequired(true)
            .HasDefaultValue(true)
            .HasColumnName("is_active")
            .HasColumnType("bit");
        
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
            .HasMany(u => u.BodyMeasurements)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId);
    }
}