using Tournament.Application.Abstraction.Messaging;

namespace Tournament.Application.Competitions.Commands.JoinPlayerCompetition;

public class JoinPlayerCompetitionCommand : ICommand
{
    public Guid CompetitionId { get; set; }
    
    public Guid ParticipantId { get; set; }
}