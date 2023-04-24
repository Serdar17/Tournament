using Tournament.Application.Abstraction.Messaging;

namespace Tournament.Application.Competitions.Queries.GetCompetitionDetails;

public class GetCompetitionDetailsQuery : IQuery<CompetitionVm>
{
    public Guid Id { get; set; }
}