using FluentValidation;

namespace Tournament.Application.Tournament.Commands.RatingCalculate;

public class RatingCalculateCommandValidator : AbstractValidator<RatingCalculateCommand>
{
    public RatingCalculateCommandValidator()
    {
        RuleFor(command => command.CompetitionId).NotEqual(Guid.Empty);
    }
}