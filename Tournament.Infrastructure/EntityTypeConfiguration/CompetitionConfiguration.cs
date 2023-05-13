using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Domain.Models.Competitions;

namespace Tournament.Infrastructure.EntityTypeConfiguration;

public class CompetitionConfiguration : IEntityTypeConfiguration<Competition>
{
    public void Configure(EntityTypeBuilder<Competition> builder)
    {
        builder.HasKey(item => item.Id);
        builder.HasIndex(note => note.Id).IsUnique();
    }
}