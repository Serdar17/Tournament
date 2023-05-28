using FluentValidation;

namespace Tournament.Application.Tournament.Commands.SaveSchedule;

public class SaveScheduleCommandValidator : AbstractValidator<SaveScheduleCommand>
{
    public SaveScheduleCommandValidator()
    {
        RuleFor(command => command.CompetitionId)
            .NotEqual(Guid.Empty)
            .WithMessage("Уникальный id соревнования не может быть пустым");

        RuleFor(command => command.ConfirmedMatchResultLookup.FirstPlayerId)
            .NotEqual(Guid.Empty)
            .WithMessage("Уникальный id игрока не может быть пустым");

        RuleFor(command => command.ConfirmedMatchResultLookup.SecondPlayerId)
            .NotEqual(Guid.Empty)
            .WithMessage("Уникальный id игрока не может быть пустым");

        RuleFor(command => command.ConfirmedMatchResultLookup.FirstPlayerScore)
            .NotNull()
            .WithMessage("Резльтаты первого игрока не должены быть null");

        RuleFor(command => command.ConfirmedMatchResultLookup.SecondPlayerScore)
            .NotNull()
            .WithMessage("Резльтаты второго игрока не должены быть null");
        
        RuleFor(command => command.ConfirmedMatchResultLookup.FirstPlayerScore!.Scored)
            .Equal(command => command.ConfirmedMatchResultLookup.SecondPlayerScore!.Missed)
            .WithMessage("Некорректный счет");
        
        RuleFor(command => command.ConfirmedMatchResultLookup.FirstPlayerScore!.Missed)
            .Equal(command => command.ConfirmedMatchResultLookup.SecondPlayerScore!.Scored)
            .WithMessage("Некорректный счет");
    }
}