using Ardalis.Result;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Common.Exceptions;
using Tournament.Domain.Models.Competition;
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

        _player.Remove(player, cancellationToken);
        
        return Result.Success();
    }
}