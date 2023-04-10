using Microsoft.EntityFrameworkCore;
using Tournament.Models;

namespace Tournament.AppDbContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     base.OnConfiguring(optionsBuilder);
    //     optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
    // }

    public DbSet<Participant> Participants { get; set; }
}