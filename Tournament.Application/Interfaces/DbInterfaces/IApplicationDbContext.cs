﻿using Microsoft.EntityFrameworkCore;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Application.Interfaces.DbInterfaces;

public interface IApplicationDbContext
{
    DbSet<Competition> Competitions { get;  }
    
    DbSet<Player> Players { get;  }
    
    // DbSet<GameResult> GameResults { get; }
    
    DbSet<Schedule> Schedules { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}