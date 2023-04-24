using FluentValidation;

namespace Tournament.Application.Features.Players.Commands.DeletePlayer;

public class DeletePlayerCommandValidator : AbstractValidator<DeletePlayerCommand>
{
    public DeletePlayerCommandValidator()
    {
        RuleFor(command => command.CompetitionId).NotEqual(Guid.Empty);

        RuleFor(command => command.ParticipantId).NotEqual(Guid.Empty);
    }
}