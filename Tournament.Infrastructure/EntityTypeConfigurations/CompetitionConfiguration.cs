using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Infrastructure.EntityTypeConfigurations;

public class CompetitionConfiguration : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.HasKey(item => item.Id);
        builder.HasIndex(note => note.Id).IsUnique();
        
        // builder
        //     .HasMany(e => e.GameResults)
        //     .WithOne(e => e.Competition)
        //     .HasForeignKey(e => e.CompetitionId)
        //     .IsRequired(false);

        builder
            .HasMany(e => e.Schedules)
            .WithOne(e => e.Competition)
            .HasForeignKey(e => e.CompetitionId)
            .IsRequired(false);
        
        builder
            .HasMany(e => e.Players)
            .WithOne(e => e.Competition)
            .HasForeignKey(e => e.CompetitionId)
            .IsRequired(false);
    }
}