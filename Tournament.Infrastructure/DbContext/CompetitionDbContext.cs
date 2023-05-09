using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competition;
using Tournament.Infrastructure.EntityTypeConfiguration;

namespace Tournament.Data;

public sealed class CompetitionDbContext : DbContext, ICompetitionDbContext
{
    public CompetitionDbContext(DbContextOptions<CompetitionDbContext> options)
        : base(options)
    {
        // Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CompetitionConfiguration());
        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }
    

    public DbSet<Competition> Competitions { get; set; } = null!;
    
    public DbSet<Player> Players { get; set; } = null!;
    
    public DbSet<GameResult> GameResults { get; set; } = null!;
}