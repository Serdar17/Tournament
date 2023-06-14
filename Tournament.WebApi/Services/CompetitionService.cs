using Tournament.Application.Interfaces;

namespace Tournament.Services;

public class CompetitionService : ICompetitionService
{
    public Guid CurrentCompetitionId { get; set; }
}