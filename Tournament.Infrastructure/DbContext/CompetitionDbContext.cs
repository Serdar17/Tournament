using Microsoft.EntityFrameworkCore;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Competition;
using Tournament.Infrastructure.EntityTypeConfiguration;

namespace Tournament.Infrastructure.Data;

public sealed class CompetitionDbContext : Microsoft.EntityFrameworkCore.DbContext, ICompetitionDbContext
{
    public CompetitionDbContext(DbContextOptions<CompetitionDbContext> options)
        : base(options)
    {
        // Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CompetitionConfiguration());
        modelBuilder.ApplyConfiguration(new CompetitionInfoConfiguration());
        base.OnModelCreating(modelBuilder);
    }
    

    public DbSet<Competition> Competitions { get; set; } = null!;

    public DbSet<CompetitionInfo> CompetitionInfos { get; set; } = null!;
}