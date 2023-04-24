using Ardalis.Result;
using AutoMapper;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Common.Exceptions;
using Tournament.Domain.Models.Competition;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Features.Players.Queries.GetCompetitionPlayers;

public class GetCompetitionPlayersHandler : IQueryHandler<GetCompetitionPlayersQuery, PlayersVm>
{
    private readonly ICompetitionRepository _competition;
    private readonly IMapper _mapper;

    public GetCompetitionPlayersHandler(ICompetitionRepository competition, IMapper mapper)
    {
        _competition = competition;
        _mapper = mapper;
    }

    public async Task<Result<PlayersVm>> Handle(GetCompetitionPlayersQuery request, CancellationToken cancellationToken)
    {
        var competitions = await _competition.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);

        if (_competition is null)
        {
            throw new NotFoundException(nameof(Competition), request.CompetitionId);
        }

        var players = competitions.Players
            .Select(p => _mapper.Map<CompetitionPlayerLookupDto>(p))
            .ToList();

        return Result.Success(new PlayersVm() {Players = players});
    }
}