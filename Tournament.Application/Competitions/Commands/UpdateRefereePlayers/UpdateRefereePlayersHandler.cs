using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Competitions.Queries.GetRefereePlayers;
using Tournament.Application.Dto.Competitions;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Competitions.Commands.UpdateRefereePlayers;

public class UpdateRefereePlayersHandler : ICommandHandler<UpdateRefereePlayersCommand, RefereePlayerList>
{
    private readonly ICompetitionRepository _competitionRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly UserManager<ApplicationUser> _manager;
    private readonly ILogger<GetRefereePlayersListHandler> _logger;
    private readonly IMapper _mapper;
    private readonly ICompetitionDbContext _dbContext;

    public UpdateRefereePlayersHandler(ICompetitionRepository repository, IPlayerRepository playerRepository, 
        ILogger<GetRefereePlayersListHandler> logger, IMapper mapper, UserManager<ApplicationUser> manager, ICompetitionDbContext dbContext)
    {
        _competitionRepository = repository;
        _logger = logger;
        _playerRepository = playerRepository;
        _mapper = mapper;
        _manager = manager;
        _dbContext = dbContext;
    }

    public async Task<Result<RefereePlayerList>> Handle(UpdateRefereePlayersCommand request, 
        CancellationToken cancellationToken)
    {
        var competition = await _competitionRepository.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);

        if (competition is null)
        {
            _logger.LogInformation("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), request.CompetitionId);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.CompetitionId}) was not found.");
        }

        foreach (var player in request.Players)
        {
            var entity = competition.Players
                .FirstOrDefault(x => x.Id == player.PlayerId);
            
            if (entity is not null)
            {
                entity.IsBlocked = player.IsBlocked;
                entity.IsParticipation = player.IsParticipation;
                _dbContext.Players.Update(entity);
            }
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        var newCompetition = await _competitionRepository.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);
        
        var players = newCompetition.Players;

        foreach (var player in players)
        {
            player.Participant = await _manager.FindByIdAsync(player.ParticipantId);
        }

        var entities = players
            .Select(x => _mapper.Map<RefereePlayerLookup>(x))
            .ToList();
        return Result.Success(new RefereePlayerList() { Players = entities });
    }
}