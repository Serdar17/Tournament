using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;

namespace Tournament.Infrastructure.EntityTypeConfiguration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .HasOne(e => e.Player)
            .WithOne(e => e.ApplicationUser)
            .HasForeignKey<Player>(e => e.ApplicationUserId)
            .IsRequired(false);
        
        builder.HasKey(u => u.Id);
        
        builder.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
        builder.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");
        
        builder.ToTable("ApplicationUser");
        
        builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
        
        builder.Property(u => u.UserName).HasMaxLength(256);
        builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
        builder.Property(u => u.Email).HasMaxLength(256);
        builder.Property(u => u.NormalizedEmail).HasMaxLength(256);

        builder
            .HasMany<IdentityUserClaim<string>>()
            .WithOne()
            .HasForeignKey(uc => uc.UserId)
            .IsRequired();
        
        builder
            .HasMany<IdentityUserLogin<string>>()
            .WithOne()
            .HasForeignKey(ul => ul.UserId)
            .IsRequired();
        
        builder
            .HasMany<IdentityUserToken<string>>()
            .WithOne()
            .HasForeignKey(ut => ut.UserId)
            .IsRequired();
        
        builder
            .HasMany<IdentityUserRole<string>>()
            .WithOne()
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
    }
}