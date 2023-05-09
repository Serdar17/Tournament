using FluentValidation;
using Tournament.Application.Features.Players.Commands.CreatePlayer;

namespace Tournament.Application.Competitions.Commands.JoinPlayerCompetition;

public class JoinPlayerCompetitionCommandValidator : AbstractValidator<JoinPlayerCompetitionCommand>
{
    public JoinPlayerCompetitionCommandValidator()
    {
        RuleFor(command => command.CompetitionId).NotEqual(Guid.Empty);

        RuleFor(command => command.ParticipantId).NotEqual(Guid.Empty);
    }
}