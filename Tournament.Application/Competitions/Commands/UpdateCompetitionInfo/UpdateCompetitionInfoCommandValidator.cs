using FluentValidation;

namespace Tournament.Application.Competitions.Commands.UpdateCompetitionInfo;

public class UpdateCompetitionInfoCommandValidator : AbstractValidator<UpdateCompetitionInfoCommand>
{
    public UpdateCompetitionInfoCommandValidator()
    {
        RuleFor(updateCommand => updateCommand.Id).NotEqual(Guid.Empty);
        RuleFor(updateCommand => updateCommand.Title).NotEmpty();
        RuleFor(item => item.StartDateTime).GreaterThanOrEqualTo(DateTime.UtcNow);
    }
}