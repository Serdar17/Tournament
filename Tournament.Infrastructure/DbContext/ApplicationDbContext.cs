using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tournament.Domain.Models.Competition;
using Tournament.Domain.Models.Participants;

namespace Tournament.Infrastructure.DbContext;

public class ApplicationDbContext : IdentityDbContext<Participant>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        // Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
    
    public DbSet<Competition> Competitions { get; set; } = null!;
    
    public DbSet<Player> Players { get; set; } = null!;

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     base.OnConfiguring(optionsBuilder);
    //     optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    // }
}