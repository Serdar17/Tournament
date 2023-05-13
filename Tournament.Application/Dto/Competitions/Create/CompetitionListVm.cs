using Tournament.Application.Competitions.Queries.GetCompetitionList;

namespace Tournament.Application.Dto.Competitions.Create;

public class CompetitionListVm
{
    public IList<CompetitionLookupDto> Competition { get; set; }
}