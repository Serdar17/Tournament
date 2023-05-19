using Tournament.Domain.Models.Competitions;

namespace Tournament.Domain.Repositories;

public interface ICompetitionRepository
{
    Task<Competition?> GetCompetitionByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task Update(Competition competition, CancellationToken cancellationToken = default);

    void Remove(Competition competition, CancellationToken cancellationToken = default);
}