using FluentValidation;

namespace Tournament.Application.Competitions.Commands.DeleteCompetitionInfo;

public class DeleteCompetitionInfoCommandValidator : AbstractValidator<DeleteCompetitionInfoCommand>
{
    public DeleteCompetitionInfoCommandValidator()
    {
        RuleFor(deleteCommand => deleteCommand.Id).NotEqual(Guid.Empty);
    }
}