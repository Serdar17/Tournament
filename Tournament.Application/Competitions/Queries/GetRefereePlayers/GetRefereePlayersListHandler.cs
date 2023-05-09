using Ardalis.Result;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Domain.Models.Competition;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Competitions.Queries.GetRefereePlayers;

public class GetRefereePlayersListHandler : IQueryHandler<GetRefereePlayersListQuery, RefereePlayerList>
{
    private readonly ICompetitionRepository _repository;
    private readonly ILogger<GetRefereePlayersListHandler> _logger;
    private readonly IMapper _mapper;

    public GetRefereePlayersListHandler(ICompetitionRepository repository, 
        ILogger<GetRefereePlayersListHandler> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<RefereePlayerList>> Handle(GetRefereePlayersListQuery request, 
        CancellationToken cancellationToken)
    {
        var competition = await _repository.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);

        if (competition is null)
        {
            _logger.LogInformation("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), request.CompetitionId);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.CompetitionId}) was not found.");
        }
        
        var players = competition.Players
            .Select(x => _mapper.Map<RefereePlayerLookup>(x))
            .ToList();
        
        return Result.Success(new RefereePlayerList() { Players = players });
    }
}