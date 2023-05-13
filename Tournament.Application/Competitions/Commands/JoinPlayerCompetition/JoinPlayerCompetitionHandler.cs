using Ardalis.Result;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto;
using Tournament.Application.Dto.Competitions.Join;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Competitions.Commands.JoinPlayerCompetition;

public class JoinPlayerCompetitionHandler : ICommandHandler<JoinPlayerCompetitionCommand, UserWithCompetitions>
{
    private readonly IParticipantRepository _participant;
    private readonly ICompetitionRepository _competition;
    private readonly IPlayerRepository _player;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public JoinPlayerCompetitionHandler(IParticipantRepository participant, ICompetitionRepository competition, 
        IPlayerRepository player, UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _participant = participant;
        _competition = competition;
        _player = player;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<Result<UserWithCompetitions>> Handle(JoinPlayerCompetitionCommand request, CancellationToken cancellationToken)
    {
        var participant = await _participant.GetParticipantByIdAsync(request.ParticipantId.ToString());

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

        var player = new Player()
        {
            Id = Guid.NewGuid(),
            CompetitionId = competition.Id,
            Competition = competition,
            CurrentRating = participant.Rating,
            ParticipantId = participant.Id
        };
        
        await _player.Add(player, cancellationToken);

        participant.Competitions.Add(competition);
        await _userManager.UpdateAsync(participant);

        var userCompetitions = _userManager.Users
            .Include(p => p.Competitions)
            .FirstOrDefault(p => p.Id == request.ParticipantId.ToString());
        
        return Result.Success( new UserWithCompetitions()
        {
            User = _mapper.Map<UserDto>(participant),
            Competitions = userCompetitions.Competitions.Select(c => c.Id).ToList()
        });
    }
}