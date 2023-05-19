using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tournament.Infrastructure.EntityTypeConfigurations;

public class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {
        builder.HasKey(rc => rc.Id);
        
        builder.ToTable("AspNetRoleClaims");
    }
}