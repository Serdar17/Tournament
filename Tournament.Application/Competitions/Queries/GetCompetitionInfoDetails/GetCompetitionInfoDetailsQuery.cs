using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Competitions.Queries.GetCompetitionInfoDetail;

namespace Tournament.Application.Competitions.Queries.GetCompetitionInfoDetails;

public class GetCompetitionInfoDetailsQuery : IQuery<CompetitonInfoVm>
{
    public Guid Id { get; set; }
}