using TripManager.Application.Abstractions;
using TripManager.Application.Abstractions.Database.Repositories;
using TripManager.Application.Features.Users.Dto;
using TripManager.Common.Abstractions;

namespace TripManager.Application.Features.Users.Commands;

public record LoginCommand(string Email, string Password) : ICommand<AccessToken>
{
    internal sealed class Handler : ICommandHandler<LoginCommand, AccessToken>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtProvider _jwtProvider;

        public Handler(IUserRepository userRepository, IJwtProvider jwtProvider)
        {
            _userRepository = userRepository;
            _jwtProvider = jwtProvider;
        }

        public async Task<AccessToken> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email)
                ?? throw new ApplicationException("User not found");

            if (!user.Password.Verify(request.Password))
                throw new ApplicationException("Invalid password");

            return AccessToken.Create(user, _jwtProvider.Generate(user));
        }
    }
}