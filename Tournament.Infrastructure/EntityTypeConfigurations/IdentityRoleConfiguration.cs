using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tournament.Infrastructure.EntityTypeConfigurations;

public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<string>> builder)
    {
        builder.HasKey(r => r.Id);
    
        builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
    
        builder.ToTable("AspNetRoles");
    
        builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
    
        builder.Property(u => u.Name).HasMaxLength(256);
        builder.Property(u => u.NormalizedName).HasMaxLength(256);

        builder.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

        builder.HasMany<IdentityRoleClaim<string>>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
    }
}