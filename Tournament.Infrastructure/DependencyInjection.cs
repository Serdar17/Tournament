using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tournament.Application.Interfaces;
using Tournament.Domain.Models.Participant;
using Tournament.Infrastructure.Data;
using Tournament.Infrastructure.DbContext;

namespace Tournament.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetValue<string>("ConnectionString:DefaultConnection"));
        });
        
        services.AddIdentity<Participant, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddDbContext<CompetitionDbContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetValue<string>("ConnectionString:DefaultConnection"));
        });
        
        services.AddTransient<ICompetitionDbContext>(provider =>
            provider.GetService<CompetitionDbContext>());
        
        return services;
    }
}