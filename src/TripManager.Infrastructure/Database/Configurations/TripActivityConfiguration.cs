using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TripManager.Domain.Trips.Activities;
using TripManager.Infrastructure.Database.Converters;

namespace TripManager.Infrastructure.Database.Configurations;

public class TripActivityConfiguration : IEntityTypeConfiguration<TripActivity>
{
    public void Configure(EntityTypeBuilder<TripActivity> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => TripActivityId.From(x))
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Start)
            .HasConversion<DateConverter>()
            .IsRequired();

        builder.Property(x => x.End)
            .HasConversion<DateConverter>()
            .IsRequired();

        builder.OwnsOne(x => x.Location, ownedBuilder =>
        {
            ownedBuilder.WithOwner().HasForeignKey("TripActivityId");
            ownedBuilder.Property(x => x.Address)
                .IsRequired();

            ownedBuilder.Property(x => x.Coordinates)
                .IsRequired();
        });
    }
}