using Ardalis.Result;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Schedule;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Tournament.Commands.SaveSchedule;

public class SaveScheduleHandler : ICommandHandler<SaveScheduleCommand>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly ICompetitionRepository _competitionRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<SaveScheduleHandler> _logger;

    public SaveScheduleHandler(IApplicationDbContext dbContext, 
        ICompetitionRepository competitionRepository, 
        IPlayerRepository playerRepository, 
        IScheduleRepository scheduleRepository, IMapper mapper,
        ILogger<SaveScheduleHandler> logger)
    {
        _dbContext = dbContext;
        _competitionRepository = competitionRepository;
        _playerRepository = playerRepository;
        _scheduleRepository = scheduleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result> Handle(SaveScheduleCommand request, 
        CancellationToken cancellationToken)
    {
        var competition = await _competitionRepository.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);
      
        if (competition is null)
        {
            _logger.LogInformation("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), request.CompetitionId);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.CompetitionId}) was not found.");
        }

        var schedule = await _scheduleRepository.GetScheduleByIdAsync(request.ConfirmedMatchResultLookup.ScheduleId, cancellationToken);
        
        schedule.UpdateScore(request.ConfirmedMatchResultLookup.FirstPlayerScore!.Scored, 
            request.ConfirmedMatchResultLookup.FirstPlayerScore!.Missed, true, true);
                
        _scheduleRepository.Update(schedule);
        
        var firstPlayer =
            await _playerRepository.GetPlayerByIdAsync(request.ConfirmedMatchResultLookup.FirstPlayerId, cancellationToken);
        var secondPlayer = 
            await _playerRepository.GetPlayerByIdAsync(request.ConfirmedMatchResultLookup.SecondPlayerId, cancellationToken);
        
        firstPlayer.SetScore(request.ConfirmedMatchResultLookup.FirstPlayerScore.Scored, 
            request.ConfirmedMatchResultLookup.FirstPlayerScore.Scored, request.ConfirmedMatchResultLookup.SecondPlayerId);
            
        secondPlayer.SetScore(request.ConfirmedMatchResultLookup.SecondPlayerScore!.Scored, 
            request.ConfirmedMatchResultLookup.SecondPlayerScore.Missed, request.ConfirmedMatchResultLookup.FirstPlayerId);
        
        _dbContext.Players.UpdateRange(firstPlayer, secondPlayer);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
            
        return Result.Success();
    }
}