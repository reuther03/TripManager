using MediatR;
using TripManager.Common.Abstractions;

public class TestCommandHandler : IQueryHandler<TestQuery, string>
{
    public Task<Unit> Handle(TestQuery request, CancellationToken cancellationToken)
    {
        Console.WriteLine("Test command executed");
        return Task.FromResult(Unit.Value);
    }
}