using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Competitions;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Competitions.Queries.GetRefereePlayers;

public class GetRefereePlayersListHandler : IQueryHandler<GetRefereePlayersListQuery, RefereePlayerList>
{
    private readonly ICompetitionRepository _repository;
    private readonly ILogger<GetRefereePlayersListHandler> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _manager;

    public GetRefereePlayersListHandler(ICompetitionRepository repository, 
        ILogger<GetRefereePlayersListHandler> logger, IMapper mapper, UserManager<ApplicationUser> manager)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _manager = manager;
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

        var players = competition.Players;

        foreach (var player in players)
        {
            player.Participant = await _manager.FindByIdAsync(player.ParticipantId);
        }

        var entities = players.Select(x => _mapper.Map<RefereePlayerLookup>(x))
            .ToList();
        return Result.Success(new RefereePlayerList() { Players = entities });
    }
}