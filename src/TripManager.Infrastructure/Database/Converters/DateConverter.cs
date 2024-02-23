using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TripManager.Common.ValueObjects;

namespace TripManager.Infrastructure.Database.Converters;

public sealed class DateConverter : ValueConverter<Date, DateTimeOffset>
{
    public DateConverter() : base(
        date => date.Value,
        dateTimeOffset => new Date(dateTimeOffset)
    )
    {
    }
}