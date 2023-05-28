using Ardalis.Result;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Schedule;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Competitions.Queries.GetScheduleByCompetitionId;

public class GetScheduleByCompetitionIdHandler : IQueryHandler<GetScheduleByCompetitionIdQuery, List<ScheduleDto>>
{
    private readonly ICompetitionRepository _competitionRepository;
    private readonly IMatchResultRepository _gameResultRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly ILogger<GetScheduleByCompetitionIdHandler> _logger;
    private readonly IMapper _mapper;

    public GetScheduleByCompetitionIdHandler( 
        ICompetitionRepository competitionRepository, 
        ILogger<GetScheduleByCompetitionIdHandler> logger, 
        IPlayerRepository playerRepository, 
        IMatchResultRepository gameResultRepository, IMapper mapper)
    {
        _competitionRepository = competitionRepository;
        _logger = logger;
        _playerRepository = playerRepository;
        _gameResultRepository = gameResultRepository;
        _mapper = mapper;
    }

    public async Task<Result<List<ScheduleDto>>> Handle(GetScheduleByCompetitionIdQuery request, 
        CancellationToken cancellationToken)
    {
        var competition = await _competitionRepository.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);
      
        if (competition is null)
        {
            _logger.LogInformation("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), request.CompetitionId);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.CompetitionId}) was not found.");
        }

        var result = new List<ScheduleDto>();
        var currentTableNumber = 1;
        foreach (var schedule in competition.Schedules.OrderBy(x => x.Id))
        {
            var p1 = await _playerRepository.GetPlayerByIdAsync(schedule.FirstPlayerId, cancellationToken);
            var p2 = await _playerRepository.GetPlayerByIdAsync(schedule.SecondPlayerId, cancellationToken);
            
            var scheduleDto = new ScheduleDto(
                schedule.Id,
                _mapper.Map<PlayerDto>(p1), 
                _mapper.Map<PlayerDto>(p2), 
                currentTableNumber);
            
            if (schedule.HasPlayed)
            {
                scheduleDto.FirstPlayerScore = schedule.FirstPlayerScore;
                scheduleDto.SecondPlayerScore = schedule.SecondPlayerScore;
                scheduleDto.HasPlayed = true;
                scheduleDto.IsConfirmed = true;
            }
            result.Add(scheduleDto);
            
            currentTableNumber = currentTableNumber == competition.TableCount
                ? 1
                : currentTableNumber + 1;
        }
        
        return Result.Success(result);
    }
}