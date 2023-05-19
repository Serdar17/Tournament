using Microsoft.EntityFrameworkCore;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;

namespace Tournament.Infrastructure.Repositories;

public class CompetitionRepository : ICompetitionRepository
{
    private readonly IApplicationDbContext _dbContext;

    public CompetitionRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Competition?> GetCompetitionByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Competitions
            .Include(c => c.Players)
            .Include(c => c.Schedules)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task Update(Competition competition, CancellationToken cancellationToken = default)
    {
        _dbContext.Competitions.Update(competition);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Remove(Competition competition, CancellationToken cancellationToken = default)
    {
        _dbContext.Competitions.Remove(competition);
        _dbContext.SaveChangesAsync(cancellationToken);
    }
}