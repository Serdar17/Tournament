using Tournament.Domain.Models.Competitions;

namespace Tournament.Domain.Repositories;

public interface IScheduleRepository
{
    Task AddAsync(Schedule schedule, CancellationToken cancellationToken = default);

    Task<Schedule?> GetScheduleByIdAsync(int id, CancellationToken cancellationToken = default);

    IEnumerable<Schedule> GetSchedulesByCompetitionId(Guid competitionId, Guid playerId);

    void Update(Schedule schedule);

    Task SaveAsync(CancellationToken cancellationToken = default);
}