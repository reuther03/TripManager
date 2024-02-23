using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripManager.Common.ValueObjects;
using TripManager.Domain.Trips;
using TripManager.Domain.Trips.ValueObjects;
using TripManager.Domain.Users.ValueObjects;
using TripManager.Infrastructure.Database.Converters;

namespace TripManager.Infrastructure.Database.Configurations;

public class TripConfigurations : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => TripId.From(x))
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Country)
            .HasConversion(x => x.Value, x => new Country(x))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, x => new Description(x))
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Start)
            .HasConversion<DateConverter>()
            .IsRequired();

        builder.Property(x => x.End)
            .HasConversion<DateConverter>()
            .IsRequired();

        builder.OwnsOne(x => x.Settings, ownedBuilder =>
        {
            ownedBuilder.Property(x => x.Description)
                .HasConversion(x => x.Value, x => new Description(x))
                .IsRequired();

            ownedBuilder.Property(x => x.Budget)
                .HasPrecision(10, 2)
                .IsRequired();
        });

        builder.Navigation(x => x.Activities).UsePropertyAccessMode(PropertyAccessMode.Field);
        builder.HasMany(x => x.Activities)
            .WithOne()
            .HasForeignKey(x => x.TripId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}