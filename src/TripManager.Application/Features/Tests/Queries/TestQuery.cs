using TripManager.Common.Abstractions;

namespace TripManager.Application.Features.Tests.Queries;

public class GetRandomTextQuery : IQuery<string>
{
    internal sealed class Handler : IQueryHandler<GetRandomTextQuery, string>
    {
        public Task<string> Handle(GetRandomTextQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult($"Hello World from {nameof(GetRandomTextQuery)}!");
        }
    }
}