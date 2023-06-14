using Ardalis.Result;
using Microsoft.Extensions.Logging;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Tournament.Commands.StartCompetition;

public class StartCompetitionHandler : ICommandHandler<StartCompetitionCommand>
{
    private readonly ICompetitionRepository _competitionRepository;
    private readonly ICompetitionService _competitionService;
    private readonly ILogger<StartCompetitionHandler> _logger;

    public StartCompetitionHandler(ICompetitionRepository competitionRepository, 
        ILogger<StartCompetitionHandler> logger,
        ICompetitionService competitionService)
    {
        _competitionRepository = competitionRepository;
        _logger = logger;
        _competitionService = competitionService;
    }


    public async Task<Result> Handle(StartCompetitionCommand request, 
        CancellationToken cancellationToken)
    {
        var competition = await _competitionRepository.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);
        
        if (competition is null)
        {
            _logger.LogInformation("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), request.CompetitionId);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.CompetitionId}) was not found.");
        }

        if (!Guid.Empty.Equals(_competitionService.CurrentCompetitionId))
        {
            _logger.LogInformation("You can only run one competition");
            
            return Result.Error($"You can only run one competition.");
        }

        _competitionService.CurrentCompetitionId = competition.Id;
        
        return Result.Success();
    }
}