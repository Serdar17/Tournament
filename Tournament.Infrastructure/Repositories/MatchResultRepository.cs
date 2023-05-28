using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;

namespace Tournament.Infrastructure.Repositories;

public class MatchResultRepository : IMatchResultRepository
{
    private readonly IApplicationDbContext _dbContext;
    
    public MatchResultRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public MatchResult? GetMatchResultByPlayersId(Guid firstPlayerId, Guid secondPlayerId)
    {
        return _dbContext.MatchResults
            .FirstOrDefault(x => x.FirstPlayerId == firstPlayerId && x.SecondPlayerId == secondPlayerId ||
                                 x.FirstPlayerId == secondPlayerId && x.SecondPlayerId == firstPlayerId);
    }

    public async Task AddAsync(MatchResult matchResult, CancellationToken cancellationToken = default)
    {
        await _dbContext.MatchResults.AddAsync(matchResult, cancellationToken);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task Update(MatchResult matchResult)
    {
        throw new NotImplementedException();
    }
}