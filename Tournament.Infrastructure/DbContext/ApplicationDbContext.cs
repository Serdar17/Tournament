using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;

namespace Tournament.Infrastructure.DbContext;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.Entity<Competition>()
        //     .HasMany(e => e.ApplicationUser)
        //     .WithMany(e => e.Competitions)
        //     .IsRequired(false);
        base.OnModelCreating(builder);
    }

}