using Ardalis.Result;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Tournament.Application.Abstraction.Messaging;
using Tournament.Application.Dto.Account;
using Tournament.Application.Dto.Competitions.ConfirmedMatchResult;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;

namespace Tournament.Application.Tournament.Queries.GetConfirmedMatchResults;

public class GetConfirmedMatchResultHandler : IQueryHandler<GetConfirmedMatchResultQuery, ConfirmedMatchResultDto>
{
    private readonly IMatchResultRepository _matchResultRepository;
    private readonly ICompetitionRepository _competitionRepository;
    private readonly ILogger<GetConfirmedMatchResultHandler> _logger;
    private readonly IMapper _mapper;

    public GetConfirmedMatchResultHandler(IMatchResultRepository matchResultRepository, 
        ICompetitionRepository competitionRepository,
        ILogger<GetConfirmedMatchResultHandler> logger, 
        IMapper mapper)
    {
        _matchResultRepository = matchResultRepository;
        _competitionRepository = competitionRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<ConfirmedMatchResultDto>> Handle(GetConfirmedMatchResultQuery request, 
        CancellationToken cancellationToken)
    {
        var competition = await _competitionRepository.GetCompetitionByIdAsync(request.CompetitionId, cancellationToken);

        if (competition is null)
        {
            _logger.LogInformation("Entity \"{Name}\" {@CompetitionId} was not found",
                nameof(Competition), request.CompetitionId);
            
            return Result.NotFound($"Entity \"{nameof(Competition)}\" ({request.CompetitionId}) was not found.");
        }

        var result = new List<ConfirmedMatchResultLookup>();

        foreach (var schedule in competition.Schedules.Where(x => !x.IsConfirmed))
        {
            var confirmedResult = _mapper.Map<ConfirmedMatchResultLookup>(schedule);

            // var matchResult = competition.MatchResults.FirstOrDefault(x => x.FirstPlayerId == schedule.FirstPlayerId || x.SecondPlayerId == schedule.FirstPlayerId);
            // if (schedule.FirstPlayerScored is not null)
            // {
            //     confirmedResult.FirstPlayerScore = schedule.FirstPlayerScored;
            // }
            // confirmedResult.FirstPlayerId = schedule.FirstPlayerId;
            //
            // if (schedule.SecondPlayerScored is not null)
            // {
            //     confirmedResult.SecondPlayerScore = schedule.SecondPlayerScored;
            // }
            // confirmedResult.SecondPlayerId = schedule.SecondPlayerId;
            
            // if (matchResult is not null)
            // {
            //     
            // }
            // confirmedResult.FirstPlayerId = schedule.FirstPlayerId;
            
            // matchResult = competition.MatchResults.FirstOrDefault(x => x.FirstPlayerId == schedule.SecondPlayerId || x.SecondPlayerId == schedule.SecondPlayerId);
            // if (matchResult is not null)
            // {
            //     confirmedResult.SecondPlayerScore = matchResult.SecondPlayerScore;
            // }
            // confirmedResult.SecondPlayerId = schedule.SecondPlayerId;
            
            var player =  competition.Players.FirstOrDefault(x => x.Id == schedule.FirstPlayerId);
            
            confirmedResult.FirstPlayerModel = _mapper.Map<PlayerModel>(player);
            
            player =  competition.Players.FirstOrDefault(x => x.Id == schedule.SecondPlayerId);
            confirmedResult.SecondPlayerModel = _mapper.Map<PlayerModel>(player);

            confirmedResult.ScheduleId = schedule.Id;
            
            result.Add(confirmedResult);
        }
        
        return Result.Success(new ConfirmedMatchResultDto(){ ResultLookups = result });
    }
}