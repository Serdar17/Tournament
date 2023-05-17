using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Competitions.Queries.GetRefereePlayers;
using Tournament.Application.Dto.Competitions.Join;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Competitions.Queries.GetJoinedPlayersById;

public class GetJoinedPlayersByIdHandler : IQueryHandler<GetJoinedPlayersByIdQuery, JoinedPlayerList>
{
    private readonly ICompetitionRepository _repository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetRefereePlayersListHandler> _logger;

    public GetJoinedPlayersByIdHandler(ICompetitionRepository repository, 
        ILogger<GetRefereePlayersListHandler> logger, IMapper mapper, 
        IPlayerRepository playerRepository)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _playerRepository = playerRepository;
    }
    
    public async Task<Result<JoinedPlayerList>> Handle(GetJoinedPlayersByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var competition = await _repository.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);
        
        if (competition is null)
        {
            _logger.LogInformation("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), request.CompetitionId);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.CompetitionId}) was not found.");
        }

        // var players = competition.Players
        //     .Where(p => p.IsParticipation)
        //     .ToList();
        
        var players = await _playerRepository.GetPlayersByCompetitionId(competition.Id, cancellationToken);
        
        var entities = players
            .Select(x => _mapper.Map<JoinedPlayersLookup>(x))
            .ToList();

        return Result.Success(new JoinedPlayerList() { Players = entities });
    }
}