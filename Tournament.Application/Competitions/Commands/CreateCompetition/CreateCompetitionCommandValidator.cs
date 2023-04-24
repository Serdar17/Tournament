using FluentValidation;

namespace Tournament.Application.Competitions.Commands.CreateCompetition;

public class CreateCompetitionInfoCommandValidator : AbstractValidator<CreateCompetitionCommand>
{
    public CreateCompetitionInfoCommandValidator()
    {
        RuleFor(command => command.Title).NotEmpty();
        // RuleFor(createCommand => createCommand.Description).NotEmpty();
        RuleFor(command => command.StartDateTime).GreaterThanOrEqualTo(DateTime.UtcNow);
        RuleFor(command => command.Description).NotEmpty();
    }
}