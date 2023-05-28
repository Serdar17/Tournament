// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Tournament.Domain.Models.Competitions;
//
// namespace Tournament.Infrastructure.EntityTypeConfigurations;
//
// public class MatchResultConfiguration : IEntityTypeConfiguration<MatchResult>
// {
//     public void Configure(EntityTypeBuilder<MatchResult> builder)
//     {
//         builder.HasKey(x => x.Id);
//
//         builder.OwnsOne(x => x.FirstPlayerScore);
//         builder.OwnsOne(x => x.SecondPlayerScore);
//     }
// }