using Microsoft.EntityFrameworkCore;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Interfaces.DbInterfaces;

public interface ICompetitionDbContext
{
    DbSet<Competition> Competitions { get; set; }
    
    DbSet<Player> Players { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}