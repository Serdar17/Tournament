using Tournament.Application.Abstraction.Messaging;

namespace Tournament.Application.Features.Players.Queries.GetCompetitionPlayers;

public class GetCompetitionPlayersQuery : IQuery<PlayersVm>
{
    public Guid CompetitionId { get; set; }
}