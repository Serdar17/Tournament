using Tournament.Domain.Models.Competitions;

namespace Tournament.Domain.Repositories;

public interface IPlayerRepository
{
    Task<Player?> GetPlayerByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<Player>> GetPlayersByCompetitionId(Guid competitionId, CancellationToken cancellationToken = default);

    Task Add(Player player, CancellationToken cancellationToken = default);

    Task Update(Player player, CancellationToken cancellationToken = default);
    
    Task UpdateRange(List<Player> players, CancellationToken cancellationToken = default);

    Task Remove(Player player, CancellationToken cancellationToken = default);
}