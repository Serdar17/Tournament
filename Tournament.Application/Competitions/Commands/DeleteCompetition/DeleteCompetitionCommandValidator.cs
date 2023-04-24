using FluentValidation;

namespace Tournament.Application.Competitions.Commands.DeleteCompetition;

public class DeleteCompetitionCommandValidator : AbstractValidator<DeleteCompetitionCommand>
{
    public DeleteCompetitionCommandValidator()
    {
        RuleFor(command => command.Id).NotEqual(Guid.Empty);
    }
}