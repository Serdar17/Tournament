using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Competitions;

namespace Tournament.Application.Competitions.Commands.UpdateRefereePlayers;

public class UpdateRefereePlayersCommand : ICommand<RefereePlayerList>
{
    public Guid CompetitionId { get; set; }
    
    public IList<RefereePlayerLookup> Players { get; set; }
}