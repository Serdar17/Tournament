using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tournament.Infrastructure.EntityTypeConfigurations;

public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasKey(r => new { r.UserId, r.RoleId });
        
        builder.ToTable("AspNetUserRoles");
    }
}