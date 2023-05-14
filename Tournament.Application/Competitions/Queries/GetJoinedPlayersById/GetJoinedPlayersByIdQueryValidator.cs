using FluentValidation;

namespace Tournament.Application.Competitions.Queries.GetJoinedPlayersById;

public class GetJoinedPlayersByIdQueryValidator : AbstractValidator<GetJoinedPlayersByIdQuery>
{
    public GetJoinedPlayersByIdQueryValidator()
    {
        RuleFor(query => query.CompetitionId).NotEqual(Guid.Empty);
    }
}