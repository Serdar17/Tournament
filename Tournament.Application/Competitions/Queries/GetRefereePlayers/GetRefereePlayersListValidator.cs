using FluentValidation;

namespace Tournament.Application.Competitions.Queries.GetRefereePlayers;

public class GetRefereePlayersListValidator : AbstractValidator<GetRefereePlayersListQuery>
{
    public GetRefereePlayersListValidator()
    {
        RuleFor(query => query.CompetitionId).NotEqual(Guid.Empty);
    }
}