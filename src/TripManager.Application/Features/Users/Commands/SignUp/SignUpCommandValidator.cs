using FluentValidation;

namespace TripManager.Application.Features.Users.Commands.SignUp;

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();

        RuleFor(x => x.Username).NotEmpty().MaximumLength(30);
    }
}