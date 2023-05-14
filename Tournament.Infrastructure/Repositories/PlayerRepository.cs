using Microsoft.EntityFrameworkCore;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;

namespace Tournament.Infrastructure.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly ICompetitionDbContext _dbContext;

    public PlayerRepository(ICompetitionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Player?> GetPlayerByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Players
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public Task<IEnumerable<Player>> GetPlayersByCompetitionId(Guid competitionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task Add(Player player, CancellationToken cancellationToken = default)
    {
        _dbContext.Players.Add(player);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async void Update(Player player, CancellationToken cancellationToken = default)
    {
        _dbContext.Players.Update(player);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Remove(Player player, CancellationToken cancellationToken = default)
    {
        _dbContext.Players.Remove(player);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}