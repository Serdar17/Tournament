﻿using Microsoft.EntityFrameworkCore;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competition;
using Tournament.Domain.Repositories;

namespace Tournament.Infrastructure.Repositories;

public class CompetitionRepository : ICompetitionRepository
{
    private readonly ICompetitionDbContext _dbContext;

    public CompetitionRepository(ICompetitionDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Competition?> GetCompetitionByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Competitions
            .Include(p => p.Players)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Update(Competition competition, Player player, CancellationToken cancellationToken = default)
    {
        _dbContext.Competitions.Update(competition);
        _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Save(Competition competition, CancellationToken cancellationToken = default)
    {
        _dbContext.Competitions.Update(competition);
        _dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Remove(Competition competition, CancellationToken cancellationToken = default)
    {
        _dbContext.Competitions.Remove(competition);
        _dbContext.SaveChangesAsync(cancellationToken);
    }
}