using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Repositories;

namespace Tournament.Infrastructure.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly IApplicationDbContext _dbContext;

    public ScheduleRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(Schedule schedule)
    {
        _dbContext.Schedules.Add(schedule);
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}