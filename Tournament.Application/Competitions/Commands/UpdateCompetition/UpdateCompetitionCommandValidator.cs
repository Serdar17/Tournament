using FluentValidation;

namespace Tournament.Application.Competitions.Commands.UpdateCompetition;

public class UpdateCompetitionCommandValidator : AbstractValidator<UpdateCompetitionCommand>
{
    public UpdateCompetitionCommandValidator()
    {
        RuleFor(command => command.Id).NotEqual(Guid.Empty);
        RuleFor(command => command.Title).NotEmpty();
        RuleFor(command => command.StartDateTime).GreaterThanOrEqualTo(DateTime.UtcNow);
    }
}