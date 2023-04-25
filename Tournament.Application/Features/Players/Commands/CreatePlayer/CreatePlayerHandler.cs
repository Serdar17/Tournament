using Ardalis.Result;
using Serilog;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Domain.Models.Competition;
using Tournament.Domain.Models.Participants;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Features.Players.Commands.CreatePlayer;

public class CreatePlayerHandler : ICommandHandler<CreatePlayerCommand>
{
    private readonly IParticipantRepository _participant;
    private readonly ICompetitionRepository _competition;
    private readonly IPlayerRepository _player;

    public CreatePlayerHandler(IParticipantRepository participant, ICompetitionRepository competition, 
        IPlayerRepository player)
    {
        _participant = participant;
        _competition = competition;
        _player = player;
    }

    public async Task<Result> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
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

        var player = new Player()
        {
            Id = Guid.NewGuid(),
            ParticipantId = participant.Id,
            CompetitonId = competition.Id,
            Competition = competition,
            CurrentRating = participant.Rating
        };
        
        _player.Add(player, cancellationToken);
        
        _competition.Update(competition, player, cancellationToken);
        
        return Result.Success();
    }
}