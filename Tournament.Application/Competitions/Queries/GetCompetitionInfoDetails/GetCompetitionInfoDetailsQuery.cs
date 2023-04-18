using MediatR;
using Tournament.Application.Competitions.Queries.GetCompetitionInfoDetail;

namespace Tournament.Application.Competitions.Queries.GetCompetitionInfoDetails;

public class GetCompetitionInfoDetailsQuery : IRequest<CompetitonInfoVm>
{
    public Guid Id { get; set; }
}