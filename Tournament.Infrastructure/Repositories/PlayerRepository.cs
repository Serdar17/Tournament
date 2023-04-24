using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competition;
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
    
    public void Add(Player player, CancellationToken cancellationToken = default)
    {
        _dbContext.Players.Add(player);
        _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Update(Player player, CancellationToken cancellationToken = default)
    {
        _dbContext.Players.Update(player);
        _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Remove(Player player, CancellationToken cancellationToken = default)
    {
        _dbContext.Players.Remove(player);
        _dbContext.SaveChangesAsync(cancellationToken);
    }
}