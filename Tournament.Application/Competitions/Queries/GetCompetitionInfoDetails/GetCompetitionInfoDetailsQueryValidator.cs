using FluentValidation;

namespace Tournament.Application.Competitions.Queries.GetCompetitionInfoDetails;

public class GetCompetitionInfoDetailsQueryValidator : AbstractValidator<GetCompetitionInfoDetailsQuery>
{
    public GetCompetitionInfoDetailsQueryValidator()
    {
        RuleFor(query => query.Id).NotEqual(Guid.Empty);
    }
}