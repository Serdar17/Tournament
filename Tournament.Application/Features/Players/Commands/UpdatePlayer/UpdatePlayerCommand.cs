using Tournament.Application.Abstraction.Messaging;

namespace Tournament.Application.Features.Players.Commands.UpdatePlayer;

public class UpdatePlayerCommand : ICommand
{
    public Guid ParticipantId { get; set; }

    public Guid CompetitionId { get; set; }
    
    public Guid PlayerId { get; set; }
    
    public long CurrentRating { get; set; }
    
    public bool IsParticipation { get; set; }

    public int Scored { get; set; }
    
    public int Missed { get; set; }

}