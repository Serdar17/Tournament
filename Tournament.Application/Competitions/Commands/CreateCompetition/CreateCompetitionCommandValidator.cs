using FluentValidation;

namespace Tournament.Application.Competitions.Commands.CreateCompetition;

public class CreateCompetitionInfoCommandValidator : AbstractValidator<CreateCompetitionCommand>
{
    public CreateCompetitionInfoCommandValidator()
    {
        RuleFor(command => command.Title)
            .NotEmpty()
            .WithMessage("Поле название должно быть заполнено");
        
        RuleFor(createCommand => createCommand.Description)
            .NotEmpty()
            .WithMessage("Поле описание должно быть заполнено");
        
        RuleFor(command => command.StartDateTime)
            .GreaterThanOrEqualTo(DateTime.Now)
            .WithMessage($"Поле время не корректно, дожно быть больше чем {DateTime.Now}");
        
        RuleFor(command => command.PlaceDescription)
            .NotEmpty()
            .WithMessage("Поле описания места проведения должно быть заполнено");
    }
}