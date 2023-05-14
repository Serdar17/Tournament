﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tournament.Domain.Models.Competitions;
using Tournament.Domain.Models.Participants;
using Tournament.Infrastructure.EntityTypeConfiguration;

namespace Tournament.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Competition> Competitions { get; set; } = null!;
    
    public DbSet<Player> Players { get; set; } = null!;
    
    public DbSet<GameResult> GameResults { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // builder.Entity<ApplicationUser>()
        //     .HasOne(e => e.Competition)
        //     .WithMany(e => e.ApplicationUsers)
        //     .HasForeignKey(e => e.CompetitionId)
        //     .IsRequired(false);
        builder.Entity<ApplicationUser>()
            .HasOne(e => e.Player)
            .WithOne(e => e.ApplicationUser)
            .HasForeignKey<Player>(e => e.ApplicationUserId)
            .IsRequired(false);
        
        builder.Entity<ApplicationUser>(b =>
        {
            // Primary key
            b.HasKey(u => u.Id);
        
            // Indexes for "normalized" username and email, to allow efficient lookups
            b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");
        
            // Maps to the AspNetUsers table
            b.ToTable("ApplicationUser");
        
            // A concurrency token for use with the optimistic concurrency checking
            b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
        
            // Limit the size of columns to use efficient database types
            b.Property(u => u.UserName).HasMaxLength(256);
            b.Property(u => u.NormalizedUserName).HasMaxLength(256);
            b.Property(u => u.Email).HasMaxLength(256);
            b.Property(u => u.NormalizedEmail).HasMaxLength(256);
        
            // The relationships between User and other entity types
            // Note that these relationships are configured with no navigation properties
            
        
            // Each User can have many UserClaims
            b.HasMany<IdentityUserClaim<string>>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
        
            // Each User can have many UserLogins
            b.HasMany<IdentityUserLogin<string>>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
        
            // Each User can have many UserTokens
            b.HasMany<IdentityUserToken<string>>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();
        
            // Each User can have many entries in the UserRole join table
            b.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
        });
        
        builder.Entity<IdentityUserClaim<string>>(b =>
        {
            // Primary key
            b.HasKey(uc => uc.Id);
        
            // Maps to the AspNetUserClaims table
            b.ToTable("AspNetUserClaims");
        });
        
        builder.Entity<IdentityUserLogin<string>>(b =>
        {
            // Composite primary key consisting of the LoginProvider and the key to use
            // with that provider
            b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
        
            // Limit the size of the composite key columns due to common DB restrictions
            b.Property(l => l.LoginProvider).HasMaxLength(128);
            b.Property(l => l.ProviderKey).HasMaxLength(128);
        
            // Maps to the AspNetUserLogins table
            b.ToTable("AspNetUserLogins");
        });
        
        builder.Entity<IdentityUserToken<string>>(b =>
        {
            // Composite primary key consisting of the UserId, LoginProvider and Name
            b.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        
            // Limit the size of the composite key columns due to common DB restrictions
            b.Property(t => t.LoginProvider).HasMaxLength(255);
            b.Property(t => t.Name).HasMaxLength(255);
        
            // Maps to the AspNetUserTokens table
            b.ToTable("AspNetUserTokens");
        });
        
        builder.Entity<IdentityRole<string>>(b =>
        {
            // Primary key
            b.HasKey(r => r.Id);
        
            // Index for "normalized" role name to allow efficient lookups
            b.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();
        
            // Maps to the AspNetRoles table
            b.ToTable("AspNetRoles");
        
            // A concurrency token for use with the optimistic concurrency checking
            b.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
        
            // Limit the size of columns to use efficient database types
            b.Property(u => u.Name).HasMaxLength(256);
            b.Property(u => u.NormalizedName).HasMaxLength(256);
        
            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties
        
            // Each Role can have many entries in the UserRole join table
            b.HasMany<IdentityUserRole<string>>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
        
            // Each Role can have many associated RoleClaims
            b.HasMany<IdentityRoleClaim<string>>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();
        });
        
        
        builder.Entity<IdentityRoleClaim<string>>(b =>
        {
            // Primary key
            b.HasKey(rc => rc.Id);
        
            // Maps to the AspNetRoleClaims table
            b.ToTable("AspNetRoleClaims");
        });
        
        builder.Entity<IdentityUserRole<string>>(b =>
        {
            // Primary key
            b.HasKey(r => new { r.UserId, r.RoleId });
        
            // Maps to the AspNetUserRoles table
            b.ToTable("AspNetUserRoles");
        });
        
        // base.OnModelCreating(builder);
    }

}