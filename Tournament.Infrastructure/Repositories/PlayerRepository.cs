using Microsoft.EntityFrameworkCore;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;

namespace Tournament.Infrastructure.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly IApplicationDbContext _dbContext;

    public PlayerRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Player?> GetPlayerByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Players
            .Include(p => p.ApplicationUser)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Player>> GetPlayersByCompetitionId(Guid competitionId, 
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Players
            .Where(p => p.CompetitionId == competitionId)
            .Include(p => p.ApplicationUser)
            .ToListAsync(cancellationToken: cancellationToken);
    }

    public async Task Add(Player player, CancellationToken cancellationToken = default)
    {
        _dbContext.Players.Add(player);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(Player player, CancellationToken cancellationToken = default)
    {
        _dbContext.Players.Update(player);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRange(List<Player> players, CancellationToken cancellationToken = default)
    {
        _dbContext.Players.UpdateRange(players);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Remove(Player player, CancellationToken cancellationToken = default)
    {
        _dbContext.Players.Remove(player);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}