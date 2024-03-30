using MediatR;
using TripManager.Application.Abstractions.Database;
using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Common.Abstractions;
using TripManager.Common.Exceptions.Application;

namespace TripManager.Application.Features.Users.Commands.DeleteUser;

public record DeleteCommand(Guid UserId) : ICommand
{
    internal sealed class Handler : ICommandHandler<DeleteCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken)
                ?? throw new ApplicationValidationException("User not found");

            _userRepository.DeleteAsync(user, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}