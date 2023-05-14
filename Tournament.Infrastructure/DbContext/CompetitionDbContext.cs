using Microsoft.EntityFrameworkCore;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Infrastructure.DbContext;

public sealed class CompetitionDbContext : Microsoft.EntityFrameworkCore.DbContext, ICompetitionDbContext
{
    public CompetitionDbContext(DbContextOptions<CompetitionDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.ApplyConfiguration(new CompetitionConfiguration());
        base.OnModelCreating(modelBuilder);
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        // optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    }
    

    public DbSet<Competition> Competitions { get; set; } = null!;
    
    public DbSet<Player> Players { get; set; } = null!;
    
    public DbSet<GameResult> GameResults { get; set; } = null!;
}