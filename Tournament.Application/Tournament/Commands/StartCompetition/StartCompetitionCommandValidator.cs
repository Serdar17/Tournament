using FluentValidation;

namespace Tournament.Application.Tournament.Commands.StartCompetition;

public class StartCompetitionCommandValidator : AbstractValidator<StartCompetitionCommand>
{
    public StartCompetitionCommandValidator()
    {
        RuleFor(command => command.CompetitionId).NotEqual(Guid.Empty);
    }
}