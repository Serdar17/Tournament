using FluentValidation;

namespace Tournament.Application.Competitions.Queries.GetScheduleByCompetitionId;

public class GetScheduleByCompetitionIdQueryValidator : AbstractValidator<GetScheduleByCompetitionIdQuery>
{
    public GetScheduleByCompetitionIdQueryValidator()
    {
        RuleFor(query => query.CompetitionId).NotEqual(Guid.Empty);
    }
}