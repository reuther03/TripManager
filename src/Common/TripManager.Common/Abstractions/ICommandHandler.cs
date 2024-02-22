using MediatR;

namespace TripManager.Common.Abstractions;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Unit>
    where TCommand : ICommand;