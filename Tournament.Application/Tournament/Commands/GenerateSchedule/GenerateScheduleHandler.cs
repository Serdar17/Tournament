﻿using Ardalis.Result;
using Microsoft.Extensions.Logging;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Tournament.Commands.GenerateSchedule;

public class GenerateScheduleHandler : ICommandHandler<GenerateScheduleCommand>
{
    private readonly ICompetitionRepository _competitionRepository;
    private readonly IPlayerRepository _playerRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IScheduleService _scheduleService;
    private readonly ILogger<GenerateScheduleHandler> _logger;

    public GenerateScheduleHandler(ICompetitionRepository competitionRepository, 
        ILogger<GenerateScheduleHandler> logger, 
        IPlayerRepository playerRepository, 
        IScheduleService scheduleService, 
        IScheduleRepository scheduleRepository)
    {
        _competitionRepository = competitionRepository;
        _logger = logger;
        _playerRepository = playerRepository;
        _scheduleService = scheduleService;
        _scheduleRepository = scheduleRepository;
    }
    
    public async Task<Result> Handle(GenerateScheduleCommand request, 
        CancellationToken cancellationToken)
    {
        var competition = await _competitionRepository.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);
        
        if (competition is null)
        {
            _logger.LogInformation("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), request.CompetitionId);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.CompetitionId}) was not found.");
        }

        var availablePlayers =
            await _playerRepository.GetAvailablePlayersByCompetitionId(competition.Id, cancellationToken);
        
        var pairs = _scheduleService.GenerateSchedule(availablePlayers);

        foreach (var pair in pairs)
        {
            var schedule = new Schedule()
            {
                FirstPlayerId = pair.Item1.Id,
                SecondPlayerId = pair.Item2.Id,
                CompetitionId = competition.Id,
                Competition = competition
            };
            var firstPlayer = availablePlayers.Find(x => x.Id == pair.Item1.Id);
            firstPlayer?.PlayedGames.Add(pair.Item2.Id);

            var secondPlayer = availablePlayers.Find(x => x.Id == pair.Item2.Id);
            secondPlayer?.PlayedGames.Add(pair.Item1.Id);

            await _scheduleRepository.AddAsync(schedule, cancellationToken);
        }

        await _playerRepository.UpdateRange(availablePlayers, cancellationToken);

        await _scheduleRepository.SaveAsync(cancellationToken);

        return Result.Success();
    }
}