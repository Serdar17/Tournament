using Tournament.Application.Abstraction.Messaging;

namespace Tournament.Application.Competitions.Queries.GetRefereePlayers;

public class GetRefereePlayersListQuery : IQuery<RefereePlayerList>
{
    public Guid CompetitionId { get; set; }
}