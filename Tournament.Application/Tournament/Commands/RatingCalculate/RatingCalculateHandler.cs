using Ardalis.Result;
using Microsoft.Extensions.Logging;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;
using RatingCalculator = Tournament.Application.Solver.RatingCalculator;

namespace Tournament.Application.Tournament.Commands.RatingCalculate;

internal sealed class RatingCalculateHandler : ICommandHandler<RatingCalculateCommand>
{
    private readonly IPlayerRepository _playerRepository;
    private readonly ICompetitionRepository _competitionRepository;
    private readonly ILogger<RatingCalculateHandler> _logger;

    public RatingCalculateHandler(IPlayerRepository playerRepository, 
        ICompetitionRepository competitionRepository, 
        ILogger<RatingCalculateHandler> logger)
    {
        _playerRepository = playerRepository;
        _competitionRepository = competitionRepository;
        _logger = logger;
    }

    public async Task<Result> Handle(RatingCalculateCommand request, 
        CancellationToken cancellationToken)
    {
        var competition = await _competitionRepository.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);
        
        if (competition is null)
        {
            _logger.LogInformation("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), request.CompetitionId);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.CompetitionId}) was not found.");
        }

        var ratingCalculator = new RatingCalculator(competition.Players, competition.Schedules);
        
        var playersWithNewRating = ratingCalculator.CalculateRating();

        await _playerRepository.UpdateRange(playersWithNewRating, cancellationToken);

        return Result.Success();
    }
}