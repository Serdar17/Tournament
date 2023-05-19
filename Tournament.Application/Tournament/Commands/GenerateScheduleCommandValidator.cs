using FluentValidation;

namespace Tournament.Application.Tournament.Commands;

public class GenerateScheduleCommandValidator : AbstractValidator<GenerateScheduleCommand>
{
    public GenerateScheduleCommandValidator()
    {
        RuleFor(command => command.CompetitionId)
            .NotEqual(Guid.Empty)
            .WithMessage("Уникальный id соревнования не может быть пустым");
    }
}