using TripManager.Common.Exceptions.Domain;
using TripManager.Common.Primitives;

namespace TripManager.Domain.Users;

public record UserId : ValueObject
{
    public Guid Value { get; }

    public UserId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new DomainException("User id cannot be empty.");
        }

        Value = value;
    }

    public static implicit operator Guid(UserId userId) => userId.Value;
    public static implicit operator UserId(Guid userId) => new(userId);

    public override string ToString() => Value.ToString();
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Value;
    }
}