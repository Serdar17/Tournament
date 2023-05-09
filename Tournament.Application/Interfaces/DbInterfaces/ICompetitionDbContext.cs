using Microsoft.EntityFrameworkCore;
using Tournament.Domain.Models.Competition;

namespace Tournament.Application.Interfaces.DbInterfaces;

public interface ICompetitionDbContext
{
    DbSet<Competition> Competitions { get;  }
    
    DbSet<Player> Players { get;  }
    
    DbSet<GameResult> GameResults { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}