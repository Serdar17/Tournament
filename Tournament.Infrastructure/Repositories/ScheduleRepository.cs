using Microsoft.EntityFrameworkCore;
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

    public async Task AddAsync(Schedule schedule, CancellationToken cancellationToken = default)
    {
        await _dbContext.Schedules.AddAsync(schedule, cancellationToken);
    }

    public async Task<Schedule?> GetScheduleByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Schedules
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public IEnumerable<Schedule> GetSchedulesByCompetitionId(Guid competitionId, Guid playerId)
    {
        throw new NotImplementedException();
    }

    public void Update(Schedule schedule)
    {
        _dbContext.Schedules.Update(schedule);
    }

    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}