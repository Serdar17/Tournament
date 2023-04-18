using Microsoft.EntityFrameworkCore;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Interfaces;

public interface ICompetitionDbContext
{
    DbSet<CompetitionInfo> CompetitionInfos { get; set; }
    
    DbSet<Competition> Competitions { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}