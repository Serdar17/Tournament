using Tournament.Domain.Models.Competitions;

namespace Tournament.Domain.Repositories;

public interface IMatchResultRepository
{
    MatchResult? GetMatchResultByPlayersId(Guid firstPlayerId, Guid secondPlayerId);
    
    Task AddAsync(MatchResult matchResult, CancellationToken cancellationToken = default);
    
    Task Update(MatchResult matchResult);
    // IQueryable<GameResult> GetAll();

    // Task<GameResult?> GetGameResultByPlayersId(Guid firstPlayerId, Guid secondPlayerId,
    //     CancellationToken cancellationToken = default);
}