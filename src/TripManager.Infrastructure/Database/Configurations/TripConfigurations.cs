using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripManager.Domain.Users;
using TripManager.Domain.Users.ValueObjects;
using TripManager.Infrastructure.Database.Converters;

namespace TripManager.Infrastructure.Database.Configurations;

public class TripConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => UserId.From(x))
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Email)
            .HasConversion(x => x.Value, x => new Email(x))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Username)
            .HasConversion(x => x.Value, x => new Username(x))
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(x => x.Password)
            .HasConversion(x => x.Value, x => new Password(x))
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Fullname)
            .HasConversion(x => x.Value, x => new Fullname(x))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Role)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasConversion<DateConverter>()
            .IsRequired();

        #region Indexes

        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.Username).IsUnique();

        #endregion
    }
}