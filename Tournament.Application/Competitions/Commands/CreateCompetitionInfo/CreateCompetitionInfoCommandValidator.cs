using FluentValidation;

namespace Tournament.Application.Competitions.Commands.CreateCompetitionInfo;

public class CreateCompetitionInfoCommandValidator : AbstractValidator<CreateCompetitionInfoCommand>
{
    public CreateCompetitionInfoCommandValidator()
    {
        RuleFor(createCommand => createCommand.Title).NotEmpty();
        // RuleFor(createCommand => createCommand.Description).NotEmpty();
        RuleFor(createCommand => createCommand.StartDateTime).GreaterThanOrEqualTo(DateTime.UtcNow);
        RuleFor(createCommand => createCommand.Description).NotEmpty();
    }
}