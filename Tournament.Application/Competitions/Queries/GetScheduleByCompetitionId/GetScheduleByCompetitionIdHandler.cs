using Ardalis.Result;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Schedule;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Competitions.Queries.GetScheduleByCompetitionId;

public class GetScheduleByCompetitionIdHandler : IQueryHandler<GetScheduleByCompetitionIdQuery, List<ScheduleDto>>
{
    private readonly IScheduleService _scheduleService;
    private readonly ICompetitionRepository _competitionRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly ILogger<GetScheduleByCompetitionIdHandler> _logger;
    private readonly IMapper _mapper;

    public GetScheduleByCompetitionIdHandler(IScheduleService schedule, 
        ICompetitionRepository competitionRepository, 
        ILogger<GetScheduleByCompetitionIdHandler> logger, IMapper mapper, 
        IPlayerRepository playerRepository)
    {
        _scheduleService = schedule;
        _competitionRepository = competitionRepository;
        _logger = logger;
        _mapper = mapper;
        _playerRepository = playerRepository;
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

        var players = await _playerRepository.GetPlayersByCompetitionId(competition.Id, cancellationToken);
        var pairs = await _scheduleService.GenerateSchedule(players.ToList());
        
        var result = new List<ScheduleDto>();
        var currentTableNumber = 1;
        foreach (var pair in pairs)
        {
            result.Add(new ScheduleDto(
                _mapper.Map<PlayerDto>(pair.Item1), 
                _mapper.Map<PlayerDto>(pair.Item2), 
                currentTableNumber));
            currentTableNumber = currentTableNumber == competition.TableCount
                ? 1
                : currentTableNumber + 1;
        }

        return Result.Success(result);
    }
}