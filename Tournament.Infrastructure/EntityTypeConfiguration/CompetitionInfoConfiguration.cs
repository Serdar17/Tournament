using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Domain.Models.Competition;

namespace Tournament.Infrastructure.EntityTypeConfiguration;

public class CompetitionInfoConfiguration : IEntityTypeConfiguration<CompetitionInfo>
{
    public void Configure(EntityTypeBuilder<CompetitionInfo> builder)
    {
        builder.HasKey(item => item.Id);
        builder.HasIndex(note => note.Id).IsUnique();
        builder.Property(note => note.Title).HasMaxLength(250);
    }
}