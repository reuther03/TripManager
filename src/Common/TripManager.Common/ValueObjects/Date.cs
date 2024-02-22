using TripManager.Common.Primitives;

namespace TripManager.Common.ValueObjects;


public record Date : ValueObject
{
    public DateTimeOffset Value { get; }

    public Date(DateTimeOffset value)
    {
        Value = value;
    }

    public Date AddDays(int days) => new(Value.AddDays(days));

    public static implicit operator DateTimeOffset(Date data)
        => data.Value;

    public static implicit operator Date(DateTimeOffset value)
        => new(value);

    public static bool operator <(Date data1, Date data2)
        => data1.Value < data2.Value;

    public static bool operator >(Date data1, Date data2)
        => data1.Value > data2.Value;

    public static bool operator <=(Date data1, Date data2)
        => data1.Value <= data2.Value;

    public static bool operator >=(Date data1, Date data2)
        => data1.Value >= data2.Value;

    public static Date Now => new(DateTimeOffset.Now);

    public override string ToString() => Value.ToString("d");
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }

    public DateTime DateOnly()
        => Value.Date;


}