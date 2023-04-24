using Tournament.Application.Abstraction.Messaging;

namespace Tournament.Application.Features.Players.Commands.DeletePlayer;

public class DeletePlayerCommand: ICommand
{
    public Guid CompetitionId { get; set; }

    public Guid ParticipantId { get; set; }
    
    public Guid PlayerId { get; set; }
}