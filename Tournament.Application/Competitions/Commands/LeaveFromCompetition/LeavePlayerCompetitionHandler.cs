using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Competitions.Commands.LeaveFromCompetition;

public class LeavePlayerCompetitionHandler : ICommandHandler<LeavePlayerCompetitionCommand, UserDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICompetitionRepository _competition;
    private readonly IPlayerRepository _player;
    private readonly IMapper _mapper;

    public LeavePlayerCompetitionHandler(UserManager<ApplicationUser> userManager, ICompetitionRepository competition,
        IPlayerRepository player, IMapper mapper)
    {
        _userManager = userManager;
        _competition = competition;
        _player = player;
        _mapper = mapper;
    }
    
    public async Task<Result<UserDto>> Handle(LeavePlayerCompetitionCommand request, 
        CancellationToken cancellationToken)
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
        
        if (participant.Player is null)
        {
            // Log.Information("Entity \"{Name}\" {@CompetitionId} was not found",
            //     nameof(Competition), request.CompetitionId);
            
            return Result.Error($"Пользователь с id=\'{participant.Id}\'отсутствует на " +
                                $"соревнование с id=\'{competition.Id}\'");
        }

        // competition.Players.Remove(participant.Player);
        var player = await _player.GetPlayerByIdAsync(participant.Player.Id, cancellationToken);
        await _player.Remove(player, cancellationToken);
        participant.Player = null;

        await _competition.Update(competition, cancellationToken);

        await _userManager.UpdateAsync(participant);
        
        return Result.Success(_mapper.Map<UserDto>(participant));

    }
}