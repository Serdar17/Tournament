using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Competitions.Join;

namespace Tournament.Application.Competitions.Queries.GetJoinedPlayersById;

public class GetJoinedPlayersByIdQuery : IQuery<JoinedPlayerList>
{
    public Guid CompetitionId { get; set; }
}