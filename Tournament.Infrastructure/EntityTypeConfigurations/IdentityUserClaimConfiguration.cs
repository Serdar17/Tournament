using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tournament.Infrastructure.EntityTypeConfigurations;

public class IdentityUserClaimConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
    {
        builder.HasKey(uc => uc.Id);
        
        builder.ToTable("AspNetUserClaims");
    }
}