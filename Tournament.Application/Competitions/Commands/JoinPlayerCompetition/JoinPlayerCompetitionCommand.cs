using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Competitions.Join;

namespace Tournament.Application.Competitions.Commands.JoinPlayerCompetition;

public class JoinPlayerCompetitionCommand : ICommand<UserWithCompetitions>
{
    public Guid CompetitionId { get; set; }
    
    public Guid ParticipantId { get; set; }
}