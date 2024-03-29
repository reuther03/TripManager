using TripManager.Application.Abstractions;
using TripManager.Application.Abstractions.Database;
using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Common.Abstractions;
using TripManager.Common.Exceptions.Application;
using TripManager.Domain.Users;
using TripManager.Domain.Users.ValueObjects;
using PassValueObject = TripManager.Domain.Users.ValueObjects.Password;

namespace TripManager.Application.Features.Users.Commands.SignUp;

public record SignUpCommand(string Email, string Username, string Password, string Fullname) : ICommand<Guid>
{
    internal sealed class Handler : ICommandHandler<SignUpCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public Handler(IUserRepository userRepository, IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        public async Task<Guid> Handle(SignUpCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.ExistsWithEmailAsync(new Email(request.Email), cancellationToken) ||
                await _userRepository.ExistsWithUsernameAsync(new Username(request.Username), cancellationToken))
            {
                throw new ApplicationValidationException("Email or username already exists");
            }

            var user = User.CreateUser(new Email(request.Email), new Username(request.Username), PassValueObject.Create(request.Password), new Fullname(request.Fullname));

            await _userRepository.AddAsync(user, cancellationToken);
            await _unitOfWork.CommitAsync(cancellationToken);
            return user.Id;
        }
    }
}