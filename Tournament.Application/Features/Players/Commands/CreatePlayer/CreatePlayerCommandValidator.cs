using FluentValidation;

namespace Tournament.Application.Features.Players.Commands.CreatePlayer;

public class CreatePlayerCommandValidator : AbstractValidator<CreatePlayerCommand>
{
    public CreatePlayerCommandValidator()
    {
        RuleFor(command => command.CompetitionId).NotEqual(Guid.Empty);

        RuleFor(command => command.ParticipantId).NotEqual(Guid.Empty);
    }
}