﻿using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Competitions.Commands.JoinPlayerCompetition;

public class JoinPlayerCompetitionHandler : ICommandHandler<JoinPlayerCompetitionCommand, UserDto>
{
    private readonly ICompetitionRepository _competition;
    private readonly IPlayerRepository _player;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public JoinPlayerCompetitionHandler(ICompetitionRepository competition, 
        IPlayerRepository player, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _competition = competition;
        _player = player;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<UserDto>> Handle(JoinPlayerCompetitionCommand request, CancellationToken cancellationToken)
    {
        var participant = _userManager.Users
            .Include(u => u.Player)
            .FirstOrDefault(u => u.Id == request.ParticipantId.ToString());

        if (participant is null)
        {
            Log.Information("Entity \"{Name}\" {@ParticipantId} was not found",
                nameof(ApplicationUser), request.ParticipantId);
            
            return Result.NotFound($"Entity \"{nameof(ApplicationUser)}\" ({request.ParticipantId}) was not found.");
        }

        var competition = await _competition.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);

        if (competition is null)
        {
            Log.Information("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), request.CompetitionId);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.CompetitionId}) was not found.");
        }
        
        if (participant.Player is not null)
        {
            // Log.Information("Entity \"{Name}\" {@CompetitionId} was not found",
            //     nameof(Competition), request.CompetitionId);
            
            return Result.Error($"Пользователь с id=\'{participant.Id}\'уже зарегестрирован на " +
                                $"соревнование с id=\'{competition.Id}\'");
        }

        var player = new Player()
        {
            Id = Guid.NewGuid(),
            CompetitionId = competition.Id,
            Competition = competition,
            CurrentRating = participant.Rating
        };
        
        await _player.Add(player, cancellationToken);
        
        participant.Player = player;
        await _userManager.UpdateAsync(participant);
        
        return Result.Success(_mapper.Map<UserDto>(participant));
    }
}