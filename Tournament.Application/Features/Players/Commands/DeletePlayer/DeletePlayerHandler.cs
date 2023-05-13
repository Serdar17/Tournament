using Ardalis.Result;
using Serilog;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Features.Players.Commands.DeletePlayer;

public class DeletePlayerHandler : ICommandHandler<DeletePlayerCommand>
{
    private readonly ICompetitionRepository _competition;
    private readonly IParticipantRepository _participant;
    private readonly IPlayerRepository _player;

    public DeletePlayerHandler(ICompetitionRepository competition, IParticipantRepository participant, 
        IPlayerRepository player)
    {
        _competition = competition;
        _participant = participant;
        _player = player;
    }

    public async Task<Result> Handle(DeletePlayerCommand request, CancellationToken cancellationToken)
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

        var player = competition.Players.FirstOrDefault(p => p.Id.Equals(request.PlayerId));

        if (player is null)
        {
            Log.Information("Entity \"{Name}\" {@PlayerId} was not found",
                nameof(Player), request.PlayerId);
            
            return Result.NotFound($"Entity \"{nameof(Player)}\" ({request.PlayerId}) was not found.");
        }

        _player.Remove(player, cancellationToken);
        
        return Result.Success();
    }
}