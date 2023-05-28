using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Competitions.ConfirmedMatchResult;

namespace Tournament.Application.Tournament.Queries.GetConfirmedMatchResults;

public class GetConfirmedMatchResultQuery : IQuery<ConfirmedMatchResultDto>
{
    public Guid CompetitionId { get; set; }
}