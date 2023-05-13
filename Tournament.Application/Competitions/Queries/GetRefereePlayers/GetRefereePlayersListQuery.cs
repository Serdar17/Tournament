using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Competitions;

namespace Tournament.Application.Competitions.Queries.GetRefereePlayers;

public class GetRefereePlayersListQuery : IQuery<RefereePlayerList>
{
    public Guid CompetitionId { get; set; }
}