using FluentValidation;

namespace TripManager.Application.Features.Trips.Commands.CreateTrip;

public class CreateTripCommandValidator : AbstractValidator<CreateTripCommand>
{
    public CreateTripCommandValidator()
    {
        RuleFor(x => x.Country).NotEmpty().MaximumLength(100);

        RuleFor(x => x.Description).MaximumLength(200);

        RuleFor(x => x.Start).NotEmpty();

        RuleFor(x => x.End).NotEmpty();
        RuleFor(x => x)
            .Must(x => x.Start <= x.End)
            .WithMessage("Start date cannot be greater than end date");
    }
}