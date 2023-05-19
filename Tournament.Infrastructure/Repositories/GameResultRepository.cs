using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;

namespace Tournament.Infrastructure.Repositories;

public class GameResultRepository : IGameResultRepository
{
    // private readonly IApplicationDbContext _dbContext;
    //
    // public GameResultRepository(IApplicationDbContext dbContext)
    // {
    //     _dbContext = dbContext;
    // }
    //
    // public IQueryable<GameResult> GetAll()
    // {
    //     return _dbContext.GameResults;
    // }
    //
    // public async Task<GameResult?> GetGameResultByPlayersId(Guid firstPlayerId, Guid secondPlayerId, CancellationToken cancellationToken = default)
    // {
    //     return _dbContext
    //         .GameResults
    //         .FirstOrDefault(x => x.WinnerPlayerId == firstPlayerId && x.LoserPlayerId == firstPlayerId);
    // }
}