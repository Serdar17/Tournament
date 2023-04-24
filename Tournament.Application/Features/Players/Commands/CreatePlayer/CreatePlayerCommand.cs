using Tournament.Application.Abstraction.Messaging;

namespace Tournament.Application.Features.Players.Commands.CreatePlayer;

public class CreatePlayerCommand : ICommand
{
    public Guid CompetitionId { get; set; }
    
    public Guid ParticipantId { get; set; }
}