using FluentValidation;

namespace Tournament.Application.Competitions.Commands.UpdateRefereePlayers;

public class UpdateRefereePlayersCommandValidator : AbstractValidator<UpdateRefereePlayersCommand>
{
    public UpdateRefereePlayersCommandValidator()
    {
        RuleFor(command => command.CompetitionId).NotEqual(Guid.Empty);

        RuleForEach(command => command.Players).SetValidator(new RefereePlayerLookupValidator());
    }
}