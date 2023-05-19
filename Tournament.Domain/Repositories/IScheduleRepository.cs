using Tournament.Domain.Models.Competitions;

namespace Tournament.Domain.Repositories;

public interface IScheduleRepository
{
    void Add(Schedule schedule);

    Task SaveAsync(CancellationToken cancellationToken = default);
}