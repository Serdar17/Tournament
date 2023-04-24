using Ardalis.Result;
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
            throw new NotFoundException(nameof(Participant), request.ParticipantId);
        }

        var competition = await _competition.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);

        if (competition is null)
        {
            throw new NotFoundException(nameof(Competition), request.CompetitionId);
        }

        var player = competition.Players.FirstOrDefault(p => p.Id.Equals(request.PlayerId));

        if (player is null)
        {
            throw new NotFoundException(nameof(Player), request.PlayerId);
        }

        player.Missed = request.Missed;
        player.Scored = request.Scored;
        player.IsParticipation = request.IsParticipation;
        player.CurrentRating = request.CurrentRating;
        
        _player.Update(player, cancellationToken);
        
        return Result.Success();
    }
}