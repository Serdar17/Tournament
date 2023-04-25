using Ardalis.Result;
using Serilog;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Common.Exceptions;
using Tournament.Domain.Models.Competition;
using Tournament.Domain.Models.Participants;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Features.Players.Commands.UpdatePlayer;

public class UpdatePlayerHandler : ICommandHandler<UpdatePlayerCommand>
{
    private readonly IParticipantRepository _participant;
    private readonly ICompetitionRepository _competition;
    private readonly IPlayerRepository _player;

    public UpdatePlayerHandler(IParticipantRepository participant, ICompetitionRepository competition, IPlayerRepository player)
    {
        _participant = participant;
        _competition = competition;
        _player = player;
    }
    
    public async Task<Result> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
    {
        var participant = await _participant.GetParticipantByIdAsync(request.ParticipantId.ToString());

        if (participant is null)
        {
            Log.Information("Entity \"{Name}\" {@ParticipantId} was not found",
                nameof(Participant), request.ParticipantId);
            
            return Result.NotFound($"Entity \"{nameof(Participant)}\" ({request.ParticipantId}) was not found.");
        }

        var competition = await _competition.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);

        if (competition is null)
        {
            Log.Information("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), request.CompetitionId);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.CompetitionId}) was not found.");
        }

        var player = competition.Players.FirstOrDefault(p => p.Id.Equals(request.PlayerId));

        if (player is null)
        {
            Log.Information("Entity \"{Name}\" {@PlayerId} was not found",
                nameof(Player), request.PlayerId);
            
            return Result.NotFound($"Entity \"{nameof(Player)}\" ({request.PlayerId}) was not found.");
        }

        player.Missed = request.Missed;
        player.Scored = request.Scored;
        player.IsParticipation = request.IsParticipation;
        player.CurrentRating = request.CurrentRating;
        
        _player.Update(player, cancellationToken);
        
        return Result.Success();
    }
}