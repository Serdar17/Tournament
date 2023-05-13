using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;
using Tournament.Infrastructure.EntityTypeConfiguration;

namespace Tournament.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Competition> Competitions { get; set; } = null!;
    
    public DbSet<Player> Players { get; set; } = null!;
    
    public DbSet<GameResult> GameResults { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.Entity<ApplicationUser>()
        //     .HasMany(e => e.Competitions)
        //     .WithOne(e => e.ApplicationUser)
        //     .IsRequired(false);
        builder.Entity<Competition>()
            .HasOne(e => e.ApplicationUser)
            .WithMany(e => e.Competitions)
            .IsRequired(false);
        base.OnModelCreating(builder);
    }

}