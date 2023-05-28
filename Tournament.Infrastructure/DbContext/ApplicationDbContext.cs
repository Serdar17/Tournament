using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tournament.Application.Interfaces.DbInterfaces;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;

namespace Tournament.Infrastructure.DbContext;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureCreatedAsync();
    }

    protected override void OnModelCreating(ModelBuilder builder) =>
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    public DbSet<Competition> Competitions { get; set; } = null!;

    public DbSet<Player> Players { get; set; } = null!;

    // public DbSet<MatchResult> MatchResults { get; set; } = null!;

    public DbSet<Schedule> Schedules { get; set; } = null!;
}