using Ardalis.Result;
using AutoMapper;
using Serilog;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Domain.Models.Competitions;
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

        if (competitions is null)
        {
            Log.Information("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), request.CompetitionId);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.CompetitionId}) was not found.");
        }

        var players = competitions.Players
            .Select(p => _mapper.Map<CompetitionPlayerLookupDto>(p))
            .ToList();

        return Result.Success(new PlayersVm() {Players = players});
    }
}