using Tournament.Domain.Models.Competition;

namespace Tournament.Domain.Repositories;

public interface ICompetitionRepository
{
    Task<Competition?> GetCompetitionByIdAsync(Guid id, CancellationToken cancellationToken = default);

    void Update(Competition competition, Player player, CancellationToken cancellationToken = default);

    void Save(Competition competition, CancellationToken cancellationToken = default);

    void Remove(Competition competition, CancellationToken cancellationToken = default);
}