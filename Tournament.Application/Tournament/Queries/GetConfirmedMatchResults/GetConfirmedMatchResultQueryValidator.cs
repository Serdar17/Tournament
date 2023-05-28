using FluentValidation;

namespace Tournament.Application.Tournament.Queries.GetConfirmedMatchResults;

public class ConfirmedMatchResultQueryValidator : AbstractValidator<GetConfirmedMatchResultQuery>
{
    public ConfirmedMatchResultQueryValidator()
    {
        RuleFor(query => query.CompetitionId).NotEqual(Guid.Empty);
    }
}