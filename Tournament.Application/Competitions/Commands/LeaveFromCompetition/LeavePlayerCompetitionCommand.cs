using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto;

namespace Tournament.Application.Competitions.Commands.LeaveFromCompetition;

public class LeavePlayerCompetitionCommand : ICommand<UserDto>
{
    public Guid CompetitionId { get; set; }
    
    public Guid ParticipantId { get; set; }
}