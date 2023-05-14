using Tournament.Application.Abstraction.Messaging;

namespace Tournament.Application.Competitions.Queries.GetScheduleById;

public class GetScheduleByCompetitionId 
{
    public Guid CompetitionId { get; set; }
}