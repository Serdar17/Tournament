using FluentValidation;

namespace Tournament.Application.Competitions.Queries.GetCompetitionDetails;

public class GetCompetitionDetailsQueryValidator : AbstractValidator<GetCompetitionDetailsQuery>
{
    public GetCompetitionDetailsQueryValidator()
    {
        RuleFor(query => query.Id).NotEqual(Guid.Empty);
    }
}